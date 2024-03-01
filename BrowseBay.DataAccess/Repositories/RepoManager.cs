using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrowseBay.DataAccess.Repositories
{
    public class RepoManager<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepoManager(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var prop in includeProperties.Split
                (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop);
            }

            return query.ToList();
        }

        public virtual int GetCount(Expression<Func<T, bool>> filter)
        {
            var items = Get(filter);

            return items.Count();
        }

        public virtual T? ElementAtOrDefault(int index)
        {
            return _dbSet.ElementAtOrDefault(index);
        }

        public virtual T? Find(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            T? entity = Find(id);

            if (entity is not null)
            {
                Delete(entity);
            }
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
