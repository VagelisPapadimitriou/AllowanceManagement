using AllowanceManagement.Models;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IRankAmountRepository: IRepository<RankAmount>
    {
        IEnumerable<RankAmount> GetAll();
    }
}
