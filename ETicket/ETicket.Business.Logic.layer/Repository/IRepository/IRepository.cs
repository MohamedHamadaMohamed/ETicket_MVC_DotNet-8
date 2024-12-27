using System.Linq.Expressions;

namespace ETicket.Business.Logic.layer.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public Task CreateAsync(T entity);
        public void Update(T entity);

        public void Delete(T entity);
        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool tracked = true);
        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includeProps = null, bool trancked = true);


    }
}
