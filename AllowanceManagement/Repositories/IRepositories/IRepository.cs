using System.Linq.Expressions;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
