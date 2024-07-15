using AllowanceManagement.Data;
using AllowanceManagement.Models;
using AllowanceManagement.Repositories.IRepositories;

namespace AllowanceManagement.Repositories
{
    public class UploadedFilesRepository : Repository<UploadedFiles>, IUploadedFilesRepository
    {
        public UploadedFilesRepository(ApplicationDbContext context) : base(context) { }
        public ApplicationDbContext ApplicationDbContext => Context as ApplicationDbContext;
        public void Update()
        {

        }
    }
}
