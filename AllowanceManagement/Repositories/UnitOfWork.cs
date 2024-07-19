using AllowanceManagement.Data;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IEmployeeRepository Employee { get; private set; }
        public ICategoryPercentageRepository CategoryPercentage { get; private set; }
        public IRankAmountRepository RankAmount { get; private set; }
        public IUploadedFileRepository UploadedFile { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employee = new EmployeeRepository(_context);
            CategoryPercentage = new CategoryPercentageRepository(_context);
            RankAmount = new RankAmountRepository(_context);
            UploadedFile = new UploadedFileRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
