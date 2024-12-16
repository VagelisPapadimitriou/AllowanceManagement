using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class UploadedFileRepository : Repository<UploadedFile>, IUploadedFileRepository
    {
        public UploadedFileRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;

        public IEnumerable<UploadedFile> GetFileList()
        {
            return ApplicationDbContext.UploadedFiles.ToList();
        }
        public void Update(UploadedFile uploadedFile)
        {
            Context.Update(uploadedFile);
        }
    }
}
