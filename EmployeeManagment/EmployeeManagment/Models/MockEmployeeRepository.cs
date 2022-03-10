using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>() 
            { 
                new Employee() { Id = 1, Name ="Baljeet", Email = "baljeetsinghkochhar13@gmail.com", Department = Dept.IT},
                new Employee() { Id = 2, Name ="Deepika", Email = "deepikaraturi392@gmail.com", Department = Dept.CEO},
                new Employee() { Id = 3, Name ="Bilo", Email = "bsk1313@gmail.com", Department = Dept.HR},
                new Employee() { Id = 4, Name ="Ninda", Email = "ninda@gmail.com", Department = Dept.GHOST},
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e=> e.Id==id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name= employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
