using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            //in-memory collection
            _employeeList = new List<Employee>()
            {
              new Employee(){Id=1,Name="Mary",Email="mary@gmail.com",Department=DeptEnum.HR},
              new Employee(){Id=2,Name="David",Email="david@gmail.com",Department=DeptEnum.IT},
              new Employee(){Id=3,Name="Sara",Email="sara@gmail.com",Department=DeptEnum.Technician},
               new Employee(){Id=4,Name="Ahmad",Email="ahmad@gmail.com",Department=DeptEnum.None},
                new Employee(){Id=5,Name="Maged",Email="maged@gmail.com",Department=DeptEnum.Payroll}

            };
        }
      
        //READ OPERATION
        public Employee GetEmployee(int Id)
        {
            // throw new NotImplementedException();
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
        //READ OPERATION
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        //CREATE OPERATION
        public Employee Add(Employee employee)
        {
            //throw new NotImplementedException();
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        //UPDATE OPERATION
        public Employee Update(Employee employeeChanges)
        {
            //throw new NotImplementedException();
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }

        //DELETE OPERATION
        public Employee Delete(int id)
        {
            //throw new NotImplementedException();
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }
    }
}
