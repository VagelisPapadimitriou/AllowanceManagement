using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public IEnumerable<Employee> GetAllEmployeesWithRanksAndCategories()
        {
            return ApplicationDbContext.Employees
                 .Include(e => e.RankAmount)
                 .Include(e => e.CategoryPercentage)
                 .ToList();
        }

        public Employee GetEmployeeWithRankAndCategorie(string id)
        {
            return ApplicationDbContext.Employees
                 .Include(e => e.RankAmount)
                 .Include(e => e.CategoryPercentage)
                 .Where(e => e.AM == id)
                 .FirstOrDefault();
        }


        public void Update(Employee emp)
        {
            Context.Update(emp);
        }

    }
}
