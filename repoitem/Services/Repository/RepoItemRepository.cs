using AutoMapper;
using Microsoft.EntityFrameworkCore;
using repoitem.Data.DatabaseContext;
using repoitem.Data.Models;

namespace repoitem.Services.Repository
{
    public interface IRepoItemRepository
    {
        public Task<IEnumerable<RepoItem>> GetItemListAsync();
        public Task<RepoItem> GetItemByIdAsync(int id);
        public Task<RepoItem> AddItemAsync(RepoItem Item);


    }
    public class RepoItemRepository : IRepoItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RepoItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RepoItem> GetItemByIdAsync(int id)
        {
            return await _dbContext.RepoItems.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RepoItem>> GetItemListAsync()
        {
            return await _dbContext.RepoItems.ToListAsync();
        }

        public async Task<RepoItem> AddItemAsync(RepoItem Item)
        {
            var result = _dbContext.RepoItems.Add(Item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

    }
}