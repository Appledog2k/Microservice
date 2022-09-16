using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using repoitem.Data.Models;
using repoitem.Services.Repository;
using repoitem.SyncDataServices.Http;

namespace repoitem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoItemsController : ControllerBase
    {
        private readonly IRepoItemRepository _repoItemRepository;

        public RepoItemsController(IRepoItemRepository repoItemRepository)
        {
            _repoItemRepository = repoItemRepository;
        }

        [HttpGet("offerlist")]
        public Task<IEnumerable<RepoItem>> ItemListAsync()
        {
            var ItemList = _repoItemRepository.GetItemListAsync();
            return ItemList;

        }

        [HttpGet("getofferbyid")]
        public Task<RepoItem> GetItemByIdAsync(int Id)
        {
            return _repoItemRepository.GetItemByIdAsync(Id);
        }

    }
}