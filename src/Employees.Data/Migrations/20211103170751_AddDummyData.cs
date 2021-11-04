using Microsoft.EntityFrameworkCore.Migrations;

namespace Employees.Data.Migrations
{
    public partial class AddDummyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Employees",
                new string[] { "EmployeeId", "FirstName", "LastName", "JobTitle", "Telephone" },
                new object[] { 1, "Adam", "Ant", "CEO", "0001" });

            migrationBuilder.InsertData("Employees",
                new string[] { "EmployeeId", "FirstName", "LastName", "JobTitle", "Telephone" },
                new object[] { 2, "Brenda", "Beetle", "CFO", "0002" });

            migrationBuilder.InsertData("Employees",
                new string[] { "EmployeeId", "FirstName", "LastName", "JobTitle", "Telephone" },
                new object[] { 3, "Colin", "Caterpillar", "CIO", "0003" });

            migrationBuilder.InsertData("Employees",
                new string[] { "EmployeeId", "FirstName", "LastName", "JobTitle", "Telephone" },
                new object[] { 4, "Diana", "Dragonfly", "CTO", "0004" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from employees");
        }
    }
}
