// using Microsoft.EntityFrameworkCore;
// using repoitem.Data.DatabaseContext;

// namespace repoitem.GenericRepository
// {
//     public interface IRepository<T> where T : class
//     {
//         IEnumerable<T> GetAll();
//         T GetById(object id);
//         Task Insert(T obj);
//         void Update(T obj);
//         Task Delete(object id);
//         void Save();

//     }
//     public class Repository<T> : IRepository<T> where T : class
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly DbSet<T> _dbSet;
//         public Repository(ApplicationDbContext context)
//         {
//             _context = context;
//             _dbSet = _context.Set<T>();
//         }

//         public async Task Delete(object id)
//         {
//             T existing = await _dbSet.FindAsync(id);
//             _dbSet.Remove(existing);
//         }

//         public IEnumerable<T> GetAll()
//         {
//             return _dbSet.ToList();
//         }

//         public T GetById(object id)
//         {
//             return _dbSet.Find(id);
//         }

//         public async Task Insert(T obj)
//         {
//             await _dbSet.AddAsync(obj);
//         }

//         public void Save()
//         {
//             _context.SaveChanges();
//         }

//         public void Update(T obj)
//         {
//             _dbSet.Attach(obj);
//             _context.Entry(obj).State = EntityState.Modified;

//         }
//     }
// }