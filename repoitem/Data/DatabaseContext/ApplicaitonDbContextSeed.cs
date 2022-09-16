using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using repoitem.Data.Models;

namespace repoitem.Data.DatabaseContext
{
    public class ApplicationDbContextSeed : IEntityTypeConfiguration<RepoItem>
    {
        public void Configure(EntityTypeBuilder<RepoItem> builder)
        {
            builder.HasData(

                    new RepoItem() { Name = "Bút", Description = "Thiên Long", Cost = 1 },
                    new RepoItem() { Name = "Vở", Description = "Thiên Long", Cost = 2 },
                    new RepoItem() { Name = "Sách", Description = "Thiên Địa", Cost = 3 }
            );
        }
    }
}