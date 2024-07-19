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
        public DbSet<RankAmount> RankAmounts { get; set; }
        public DbSet<CategoryPercentage> CategoryPercentages { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }


        //Seeding
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Employee>().HasData(
        //        new Employee
        //        {
        //            AM = "M-1307",
        //            Rank = "PXHS",
        //            FirstName = "E",
        //            LastName = "Papadimitriou",
        //            Category = "B",
        //            Allowance = 570
        //        },
        //        new Employee
        //        {
        //            AM = "M-1412",
        //            Rank = "YPXOS",
        //            FirstName = "G",
        //            LastName = "Georgiou",
        //            Category = "B",
        //            Allowance = 570
        //        }
        //        );
        //}

    }
}

