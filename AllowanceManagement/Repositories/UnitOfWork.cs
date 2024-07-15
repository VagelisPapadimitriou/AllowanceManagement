using AllowanceManagement.Data;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public IEmployeeRepository Employee { get; private set; }
        public ICategoryPercentagesRepository CategoryPercentages { get; private set; }
        public IRankAllowancesRepository RankAllowances { get; private set; }
        public ISeaDaysRepository SeaDays { get; private set; }
        public IUploadedFilesRepository UploadedFiles { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employee = new EmployeeRepository(_context);
            CategoryPercentages = new CategoryPercentagesRepository(_context);
            RankAllowances = new RankAllowancesRepository(_context);
            SeaDays = new SeaDaysRepository(_context);
            UploadedFiles = new UploadedFilesRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
