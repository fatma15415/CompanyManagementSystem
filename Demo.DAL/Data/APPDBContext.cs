using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class APPDBContext : IdentityDbContext<ApplicationUser>
    {
        public APPDBContext(DbContextOptions<APPDBContext> options):base(options)
        {
                
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // call configure classes

            //this method based on meta data (call all classes that implmenting this interface)
            //based on reflition
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }



    }
}
