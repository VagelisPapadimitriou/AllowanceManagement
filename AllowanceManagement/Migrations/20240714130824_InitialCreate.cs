using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllowanceManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryPercentages",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPercentages", x => x.Category);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    AM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Allowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.AM);
                });

            migrationBuilder.CreateTable(
                name: "RankAllowances",
                columns: table => new
                {
                    RankAllowancesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankAllowances", x => x.RankAllowancesId);
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
                name: "SeaDays",
                columns: table => new
                {
                    SeaDayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeaDays", x => x.SeaDayId);
                    table.ForeignKey(
                        name: "FK_SeaDays_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "AM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeaDays_EmployeeID",
                table: "SeaDays",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPercentages");

            migrationBuilder.DropTable(
                name: "RankAllowances");

            migrationBuilder.DropTable(
                name: "SeaDays");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
