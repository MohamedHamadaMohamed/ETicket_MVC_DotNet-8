using ETicket.Business.Logic.layer.Repository.IRepository;
using ETicket.Data.Acess.layer.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace ETicket.Business.Logic.layer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();

        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _dbContext.SaveChanges();

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();

        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var prop in includeProps)
                {

                    query = query.Include(prop);
                }
            }

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool trancked = true)
        {
            return Get(filter: filter, includeProps: includeProps, tracked: trancked).FirstOrDefault();
        }
    }
}
