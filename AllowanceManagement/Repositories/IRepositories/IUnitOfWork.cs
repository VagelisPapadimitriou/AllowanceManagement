namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        ICategoryPercentagesRepository CategoryPercentages { get; }
        IRankAllowancesRepository RankAllowances { get; }
        ISeaDaysRepository SeaDays { get; }
        IUploadedFilesRepository UploadedFiles { get; }

        void Save();
    }
}
