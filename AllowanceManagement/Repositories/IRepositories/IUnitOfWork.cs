namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        ICategoryPercentageRepository CategoryPercentage { get; }
        IRankAmountRepository RankAmount { get; }
        IUploadedFileRepository UploadedFile { get; }

        void Save();
    }
}
