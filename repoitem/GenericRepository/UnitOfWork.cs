// using repoitem.Data.DatabaseContext;
// using repoitem.Data.Models;
// using repoitem.GenericRepository;

// namespace repoitem.GenericRepository
// {
//     public interface IUnitOfWork : IDisposable
//     {
//         IRepository<RepoItem> RepoItems { get; }
//         Task Save();
//     }
//     public class UnitOfWork : IUnitOfWork
//     {
//         private readonly ApplicationDbContext _context;
//         private IRepository<RepoItem> _repoItems;

//         public UnitOfWork(ApplicationDbContext context)
//         {
//             _context = context;
//         }
//         public IRepository<RepoItem> RepoItems => _repoItems ??= new Repository<RepoItem>(_context);
//         public void Dispose()
//         {
//             _context.Dispose();
//             GC.SuppressFinalize(this);
//         }
//         public async Task Save()
//         {
//             await _context.SaveChangesAsync();
//         }
//     }

// }