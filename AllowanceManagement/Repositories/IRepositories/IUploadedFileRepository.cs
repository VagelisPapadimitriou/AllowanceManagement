using AllowanceManagement.Models;

namespace AllowanceManagement.Repositories.IRepositories
{
    public interface IUploadedFileRepository : IRepository<UploadedFile>
    {
        IEnumerable<UploadedFile> GetFileList();
        void Update(UploadedFile uploadedFile);
    }
}
