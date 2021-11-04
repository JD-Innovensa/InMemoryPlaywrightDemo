using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
    public class EmployeeDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
