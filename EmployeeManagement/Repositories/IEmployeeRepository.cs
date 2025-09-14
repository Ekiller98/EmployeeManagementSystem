using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        //Get all employees
        Task<IEnumerable<Employee>> GetAllAsync();

        //Get employee by id
        Task<Employee?> GetByIdAsync(int id);

        //Add new employee(post)
        Task AddEmployeeAsync(Employee employee);

        //update employee(put)
        Task UpdateEmployeeAsync(Employee employee);

        //delete employee
        Task DeleteEmployeeAsync(int id);

    }
}
