using Microsoft.EntityFrameworkCore;
using UserManagementAPI;

namespace EFCoreInMemoryDbDemo
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UserDb");
        }
        public DbSet<User> Users { get; set; }
    }
}