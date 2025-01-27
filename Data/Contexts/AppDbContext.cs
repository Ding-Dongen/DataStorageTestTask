using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.TotalPrice)
                .HasPrecision(18, 2); // Configure decimal precision

            modelBuilder.Entity<Service>()
                .Property(s => s.HourlyPrice)
                .HasPrecision(18, 2); // Configure decimal precision

            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectNumber); // Primary key for Project

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.ProjectNumber)
                .IsUnique(); // Ensure unique project numbers

            base.OnModelCreating(modelBuilder);
        }
    }
}
