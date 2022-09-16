using System.Text;
using System.Text.Json;
using repoitem.Data.Models;

namespace repoitem.SyncDataServices.Http
{
    public interface ISyncDataClient
    {
        Task SendRepoItemToItem(RepoItemDTO repoItemDTO);
    }
    public class SyncDataClient : ISyncDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SyncDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendRepoItemToItem(RepoItemDTO repoItemDTO)
        {
            var httpContent = new StringContent(
                  JsonSerializer.Serialize(repoItemDTO),
                  Encoding.UTF8,
                  "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["ItemService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Item was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Item was NOT OK!");
            }
        }
    }
}