using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using repoitem.Data.DatabaseContext;
using repoitem.Services;
using repoitem.Services.Repository;
using repoitem.SyncDataServices.Http;

namespace repoitem
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "RepoItem", Version = "v1" });
          });

            // //* Connect String

            Console.WriteLine("--> Using PostgreSQL");
            var defaultConnection = Configuration.GetConnectionString("repoitemConnection");
            services
            .AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(defaultConnection));

            services.AddHostedService<RabbitMQServices>();

            //* add service 
            services.AddScoped<IRepoItemRepository, RepoItemRepository>();
            services.AddHttpClient<ISyncDataClient, SyncDataClient>();
            Console.WriteLine($"---> Item Service {Configuration["ItemService"]}");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Item v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
