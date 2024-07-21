using AllowanceManagement.Models;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface ICategoryPercentageRepository : IRepository<CategoryPercentage>
    {
        void Update(CategoryPercentage cp);
    }
}
