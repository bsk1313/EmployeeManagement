using System.Collections;
using System.Collections.Generic;

namespace EmployeeManagment.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        
        IEnumerable<Employee> GetAllEmployee();
        
        Employee Add(Employee employee);

        Employee Update(Employee employeeChanges);

        Employee Delete(int id);
    }
}
