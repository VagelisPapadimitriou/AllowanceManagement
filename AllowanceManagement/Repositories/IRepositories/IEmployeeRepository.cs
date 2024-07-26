using AllowanceManagement.Models;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Employee> GetAllEmployeesWithRanksAndCategories();
        Employee GetEmployeeWithRankAndCategorie(string id);
        void Update(Employee emp);
    }
}
