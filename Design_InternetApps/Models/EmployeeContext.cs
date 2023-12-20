using Microsoft.EntityFrameworkCore;

namespace Design_InternetApps.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null;
    }
}
