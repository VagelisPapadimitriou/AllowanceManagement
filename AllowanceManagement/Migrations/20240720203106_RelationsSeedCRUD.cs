using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllowanceManagement.Migrations
{
    /// <inheritdoc />
    public partial class RelationsSeedCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryPercentages",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPercentages", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "RankAmounts",
                columns: table => new
                {
                    RankAmountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankAmounts", x => x.RankAmountId);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    AM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeaDay = table.Column<int>(type: "int", nullable: false),
                    RankAmountId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.AM);
                    table.ForeignKey(
                        name: "FK_Employees_CategoryPercentages_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CategoryPercentages",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Employees_RankAmounts_RankAmountId",
                        column: x => x.RankAmountId,
                        principalTable: "RankAmounts",
                        principalColumn: "RankAmountId");
                });

            migrationBuilder.InsertData(
                table: "CategoryPercentages",
                columns: new[] { "CategoryId", "Category", "Description", "Percentage" },
                values: new object[,]
                {
                    { 1, "Α", "100% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε Φ/Γ, Υ/Β, Τ/ΠΚ και Κ/Φ", 1.00m },
                    { 2, "Β", "85% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε Α/Γ, ΠΓΥ, Ν/ΘΗ και ΠΠ", 0.85m },
                    { 3, "Γ", "70% του αρχικού ποσού για στελέχη με κύρια τοποθέτηση σε ΠΤΜ, ΣΑΠ, Π/Φ, Υ/Φ, Υ/Γ, Υ/Γ-Ω/Κ και ΠΑΤ.", 0.70m },
                    { 4, "Δ", "50% του αρχικού ποσού σε στελέχη με κύρια τοποθέτηση στη Διεύθυνση ΑΣ/ΔΕΠΑ", 0.50m }
                });

            migrationBuilder.InsertData(
                table: "RankAmounts",
                columns: new[] { "RankAmountId", "BaseAmount", "Duty", "Rank" },
                values: new object[,]
                {
                    { 1, 1015.00m, "Κυβερνήτης", "Πλοίαρχος/Αντιπλοίαρχος" },
                    { 2, 1015.00m, "-", "Διευθυντής ΑΣ/ΔΕΠΑ" },
                    { 3, 765.00m, "Κυβερνήτης", "Πλωτάρχης" },
                    { 4, 600.00m, "Κυβερνήτης", "Υποπλοίαρχος/Ανθυποπλοίαρχος/Σημαιοφόρος" },
                    { 5, 670.00m, "Υ/Αος", "Ανώτερος Αξιωματικός" },
                    { 6, 600.00m, "-", "Ανώτερος Αξιωματικός" },
                    { 7, 535.00m, "-", "Υποπλοίαρχος/Ανθυποπλοίαρχος" },
                    { 8, 510.00m, "-", "Σημαιοφόρος/Ανθυπασπιστής" },
                    { 9, 470.00m, "-", "Αρχικελευστής" },
                    { 10, 440.00m, "-", "Επικελευστής" },
                    { 11, 400.00m, "-", "Κελευστής/Δίοπος" },
                    { 12, 385.00m, "-", "ΕΠΟΠ Ναύτης/ΟΒΑ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CategoryId",
                table: "Employees",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RankAmountId",
                table: "Employees",
                column: "RankAmountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "CategoryPercentages");

            migrationBuilder.DropTable(
                name: "RankAmounts");
        }
    }
}
