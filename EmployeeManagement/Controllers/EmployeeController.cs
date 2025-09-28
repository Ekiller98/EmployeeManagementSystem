using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Repositories;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository repository)
        {
         _employeeRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var allemployees  = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(allemployees); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeebyId(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeebyId), new {id = employee.Id}, employee);
             
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeByID(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch");
            }

            await _employeeRepository.UpdateEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeebyId), new { id = employee.Id }, employee);
        }

    }
}
