using Microsoft.EntityFrameworkCore;
using Project.Domain.Models;

namespace Project.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<ReturnReason> ReturnReasons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настраиваем связь: Посылка имеет один StorageLocation
            modelBuilder.Entity<Package>()
                .HasOne(p => p.StorageLocation)   // У одной посылки есть один объект
                .WithMany()                       // А у StorageLocation может быть много посылок
                .HasForeignKey(p => p.StorageLocationId); // Связь происходит через это поле

            // Настраиваем связь: Посылка принадлежит одному Клиенту
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId);

            
           modelBuilder.Entity<Return>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmployeeId);

           modelBuilder.Entity<Operation>()
                .HasOne(o => o.Package)
                .WithMany()
                .HasForeignKey(o => o.PackageId);

           modelBuilder.Entity<Operation>()
                .HasOne(o => o.Employee)
                .WithMany()
                .HasForeignKey(o => o.EmployeeId);

            
           modelBuilder.Entity<Return>()
                .HasOne(r => r.Package)
                .WithMany()
                .HasForeignKey(r => r.PackageId);

           modelBuilder.Entity<Return>()
                .HasOne(r => r.Employee)
                .WithMany()
                .HasForeignKey(r => r.EmployeeId);

           modelBuilder.Entity<Return>()
                .HasOne(r => r.ReturnReason)
                .WithMany()
                .HasForeignKey(r => r.ReturnReasonId);
        }
    }
}
