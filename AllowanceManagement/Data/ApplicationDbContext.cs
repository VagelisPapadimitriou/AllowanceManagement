using AllowanceManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SeaDays> SeaDays { get; set; }
        public DbSet<RankAllowances> RankAllowances { get; set; }
        public DbSet<CategoryPercentages> CategoryPercentages { get; set; }
        public DbSet<UploadedFiles> UploadedFiles { get; set; }
    }
}

