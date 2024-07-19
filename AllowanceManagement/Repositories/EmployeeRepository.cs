using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;


        public void Update(Employee emp)
        {
            Context.Update(emp);
        }
    }
}
