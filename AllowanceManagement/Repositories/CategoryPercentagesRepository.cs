using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class CategoryPercentagesRepository : Repository<CategoryPercentages>, ICategoryPercentagesRepository
    {
        public CategoryPercentagesRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
        public void Update()
        {

        }
    }
}
