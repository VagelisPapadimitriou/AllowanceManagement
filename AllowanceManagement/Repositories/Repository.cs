using AllowanceManagement.Data;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }



    }
}
