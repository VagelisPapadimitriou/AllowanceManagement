using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class SeaDaysRepository : Repository<SeaDays>, ISeaDaysRepository
    {
        public SeaDaysRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
        public void Update()
        {

        }
    }
}
