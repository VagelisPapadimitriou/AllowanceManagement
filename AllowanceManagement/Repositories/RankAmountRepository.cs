using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Repositories
{
    public class RankAmountRepository : Repository<RankAmount>, IRankAmountRepository
    {
        public RankAmountRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public IEnumerable<RankAmount> GetAll()
        {
            return ApplicationDbContext.RankAmounts.ToList();
        }

        public void Update(RankAmount rankAmount)
        {
            ApplicationDbContext.RankAmounts.Update(rankAmount);
        }

    }
}
