using Microsoft.EntityFrameworkCore;
namespace EmployeeDep.Models
{
    public class AppDbContext : DbContext    
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-MS535M4\\SQLEXPRESS;Initial Catalog=ITI_G03;Integrated Security=True; Trust Server Certificate = True");


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>().HasData([new Employee { }]);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsRequired(true)
                .HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .HasOne(e=>e.Department)
                .WithMany(e=>e.Employees)
                .HasForeignKey(e => e.DepartmentId);
                
                



            base.OnModelCreating(modelBuilder);
        }

    }
}
