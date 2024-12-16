﻿using AllowanceManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllowanceManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RankAmount>().HasData(
     new RankAmount { RankAmountId = 1, Rank = "Πλοίαρχος", Duty = "Κυβερνήτης", BaseAmount = 1015.00m },
     new RankAmount { RankAmountId = 2, Rank = "Αντιπλοίαρχος", Duty = "Κυβερνήτης", BaseAmount = 1015.00m },
     new RankAmount { RankAmountId = 3, Rank = "Διευθυντής ΑΣ/ΔΕΠΑ", Duty = "-", BaseAmount = 1015.00m },
     new RankAmount { RankAmountId = 4, Rank = "Πλωτάρχης", Duty = "Κυβερνήτης", BaseAmount = 765.00m },
     new RankAmount { RankAmountId = 5, Rank = "Υποπλοίαρχος", Duty = "Κυβερνήτης", BaseAmount = 600.00m },
     new RankAmount { RankAmountId = 6, Rank = "Ανθυποπλοίαρχος", Duty = "Κυβερνήτης", BaseAmount = 600.00m },
     new RankAmount { RankAmountId = 7, Rank = "Σημαιοφόρος", Duty = "Κυβερνήτης", BaseAmount = 600.00m },
     new RankAmount { RankAmountId = 8, Rank = "Αντιπλοίαρχος", Duty = "Υ/Αος", BaseAmount = 670.00m },
     new RankAmount { RankAmountId = 9, Rank = "Πλωτάρχης", Duty = "Υ/Αος", BaseAmount = 670.00m },
     new RankAmount { RankAmountId = 10, Rank = "Αντιπλοίαρχος", Duty = "-", BaseAmount = 600.00m },
     new RankAmount { RankAmountId = 11, Rank = "Πλωτάρχης", Duty = "-", BaseAmount = 600.00m },
     new RankAmount { RankAmountId = 12, Rank = "Υποπλοίαρχος", Duty = "-", BaseAmount = 535.00m },
     new RankAmount { RankAmountId = 13, Rank = "Ανθυποπλοίαρχος", Duty = "-", BaseAmount = 535.00m },
     new RankAmount { RankAmountId = 14, Rank = "Σημαιοφόρος", Duty = "-", BaseAmount = 510.00m },
     new RankAmount { RankAmountId = 15, Rank = "Ανθυπασπιστής", Duty = "-", BaseAmount = 510.00m },
     new RankAmount { RankAmountId = 16, Rank = "Αρχικελευστής", Duty = "-", BaseAmount = 470.00m },
     new RankAmount { RankAmountId = 17, Rank = "Επικελευστής", Duty = "-", BaseAmount = 440.00m },
     new RankAmount { RankAmountId = 18, Rank = "Κελευστής", Duty = "-", BaseAmount = 400.00m },
     new RankAmount { RankAmountId = 19, Rank = "Δίοπος", Duty = "-", BaseAmount = 400.00m },
     new RankAmount { RankAmountId = 20, Rank = "ΕΠΟΠ Ναύτης", Duty = "-", BaseAmount = 385.00m },
     new RankAmount { RankAmountId = 21, Rank = "ΟΒΑ", Duty = "-", BaseAmount = 385.00m }
     );

            modelBuilder.Entity<CategoryPercentage>().HasData(
               new CategoryPercentage { CategoryId = 1, Category = "Α", Percentage = 1.00m, Description = "100% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε Φ/Γ, Υ/Β, Τ/ΠΚ και Κ/Φ" },
               new CategoryPercentage { CategoryId = 2, Category = "Β", Percentage = 0.85m, Description = "85% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε Α/Γ, ΠΓΥ, Ν/ΘΗ και ΠΠ" },
               new CategoryPercentage { CategoryId = 3, Category = "Γ", Percentage = 0.70m, Description = "70% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε ΠΤΜ, ΣΑΠ, Π/Φ, Υ/Φ, Υ/Γ, Υ/Γ-Ω/Κ και ΠΑΤ." },
               new CategoryPercentage { CategoryId = 4, Category = "Δ", Percentage = 0.50m, Description = "50% του αρχικού ποσού σε στελέχη με κύρια τοποθέτηση στη Διεύθυνση ΑΣ/ΔΕΠΑ" }
               );
        }

    }
}

