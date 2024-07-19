using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class CategoryPercentageRepository : Repository<CategoryPercentage>, ICategoryPercentageRepository
    {
        public CategoryPercentageRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
        public void Update()
        {

        }
    }
}
