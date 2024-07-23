using AllowanceManagement.Data;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AllowanceManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            return Context.Set<T>().FirstOrDefault(filter);
        }
        public T Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>> include)
        {
            IQueryable<T> query = Context.Set<T>();
            if (include != null)
            {
                query = include(query);
            }
            return query.FirstOrDefault(filter);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            Context.Set<T>().RemoveRange(entity);
            Context.SaveChanges();
        }
    }
}
