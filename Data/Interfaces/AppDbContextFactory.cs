using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Configure DbContextOptions with a hardcoded connection string
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=192.168.1.106;User ID=sa;Password=Admin#2023;Encrypt=True;Trust Server Certificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
