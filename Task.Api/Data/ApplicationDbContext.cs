using Microsoft.EntityFrameworkCore;
using Task.Api.Entites;

namespace Task.Api.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(p => p.NationalId)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(p => p.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
        }
    }
}
