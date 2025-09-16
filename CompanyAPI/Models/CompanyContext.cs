using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Models
{
    public class CompanyContext:DbContext
    {
        public DbSet<Department> Department { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CompanyAPIG2;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new Department() { Id = 1, Name = ".net", ManagerName = "ahme" });
            modelBuilder.Entity<Department>().HasData(new Department() { Id = 2, Name = "Front end", ManagerName = "mohamed" });
            modelBuilder.Entity<Department>().HasData(new Department() { Id = 3, Name = "OS", ManagerName = "ali" });
        }
    }
}
