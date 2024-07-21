using AllowanceManagement.Models;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetAllEmployeesWithRanksAndCategories();
        void Update(Employee emp);
    }
}
