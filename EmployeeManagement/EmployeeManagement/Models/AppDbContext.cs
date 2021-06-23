using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {
        //use DbContextOptions to pass configuration information to DbContext
        //constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        // the DbContext includes DbSet<TEntity> property for each entity in the model
        //to query and save instances of the Employee class
        //the LINQ queries against the DbSet<TEntity> will be translated into queries 
        //against the underlying the database.
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
           /* modelBuilder.Entity<Employee>().HasData(

                 new Employee
                 {
                     Id = 1,
                     Name = "Esraa",
                     Email = "esraa@gmail.com",
                     Department = DeptEnum.Developer
                 },
                  new Employee
                  {
                      Id = 2,
                      Name = "Mona",
                      Email = "esraa@gmail.com",
                      Department = DeptEnum.Developer
                  },
                   new Employee
                   {
                       Id = 3,
                       Name = "Esraa",
                       Email = "esraa@gmail.com",
                       Department = DeptEnum.Developer
                   },
                    new Employee
                    {
                        Id = 4,
                        Name = "Esraa",
                        Email = "esraa@gmail.com",
                        Department = DeptEnum.Developer
                    }
            );*/


             modelBuilder.Seed();

        }
    }
}
