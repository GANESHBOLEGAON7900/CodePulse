using CodePulse.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

      
        //public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //    // One-to-One relationship between Employee and Salary
        //    modelBuilder.Entity<Employee>()
        //        .HasOne(e => e.Salary)
        //        .WithOne(s => s.Employee)
        //        .HasForeignKey<Salary>(s => s.EmployeeId);

        //    // One-to-Many relationship between Employee and Photos
        //    modelBuilder.Entity<Employee>()
        //        .HasMany(e => e.Photos)
        //        .WithOne(p => p.Employee)
        //        .HasForeignKey(p => p.EmployeeId);
                base.OnModelCreating(modelBuilder);
          
        }
    }
}
