using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<Employee>().HasData(

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
                     Name = "Sara",
                     Email = "sara@gmail.com",
                     Department = DeptEnum.Developer
                 },
                  new Employee
                  {
                      Id = 4,
                      Name = "Fatma",
                      Email = "fatma@gmail.com",
                      Department = DeptEnum.Developer
                  }
          );
        }
    }
}
