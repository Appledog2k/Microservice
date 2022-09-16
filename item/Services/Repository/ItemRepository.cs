using item.Data.Models;
using Microsoft.EntityFrameworkCore;
using repoitem.Data.DatabaseContext;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace item.Services.Repository
{
    public interface IItemRepository
    {
        public Task<IEnumerable<Item>> GetItemListAsync();
        public Task<Item> GetItemByIdAsync(int id);
        public Task<Item> AddItemAsync(Item item);
        public bool SendItemOffer(ItemDTO itemDTO);

    }
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Item>> GetItemListAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _dbContext.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Item> AddItemAsync(Item Item)
        {
            var result = _dbContext.Items.Add(Item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public bool SendItemOffer(ItemDTO itemDTO)
        {
            var RabbitMQServer = "";
            var RabbitMQUserName = "";
            var RabbutMQPassword = "";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Itemion")
            {
                RabbitMQServer = Environment.GetEnvironmentVariable("RABBIT_MQ_SERVER");
                RabbitMQUserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USERNAME");
                RabbutMQPassword = Environment.GetEnvironmentVariable("RABBIT_MQ_PASSWORD");

            }
            else
            {
                RabbitMQServer = StaticConfigurationManager.AppSetting["RabbitMQ:RabbitURL"];
                RabbitMQUserName = StaticConfigurationManager.AppSetting["RabbitMQ:Username"];
                RabbutMQPassword = StaticConfigurationManager.AppSetting["RabbitMQ:Password"];
            }

            try
            {
                var factory = new ConnectionFactory()
                { HostName = RabbitMQServer, UserName = RabbitMQUserName, Password = RabbutMQPassword };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //Direct Exchange Details like name and type of exchange
                    channel.ExchangeDeclare(StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchhangeType"]);

                    //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                    channel.QueueDeclare(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"],
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //Bind Queue with Exhange and routing details
                    channel.QueueBind(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"], exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], routingKey: StaticConfigurationManager.AppSetting["RabbitMqSettings:RouteKey"]);

                    //Seriliaze object using Newtonsoft library
                    string ItemDetail = JsonConvert.SerializeObject(itemDTO);
                    var body = Encoding.UTF8.GetBytes(ItemDetail);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    //publish msg
                    channel.BasicPublish(exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"],
                                         routingKey: StaticConfigurationManager.AppSetting["RabbitMqSettings:RouteKey"],
                                         basicProperties: properties,
                                         body: body);

                    return true;
                }
            }

            catch (Exception)
            {
            }
            return false;
        }

    }
}