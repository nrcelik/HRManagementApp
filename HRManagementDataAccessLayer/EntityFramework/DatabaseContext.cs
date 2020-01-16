using HRManagementEntities;
using System.Data.Entity;

namespace HRManagementDataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Users> Users { get; set; }

        public DatabaseContext() : base("HrManagementConStr")
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, HRManagement.Migrations.Configuration>());

            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
