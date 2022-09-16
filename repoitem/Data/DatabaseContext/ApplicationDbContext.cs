using Microsoft.EntityFrameworkCore;
using repoitem.Data.Models;

namespace repoitem.Data.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<RepoItem> RepoItems { get; set; }

    }
}