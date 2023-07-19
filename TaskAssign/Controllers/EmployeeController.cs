using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeBL _bl;

        public EmployeeController(EmployeeBL bl)
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Employee> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public Employee? GetEmployee(Guid id) 
        {
            return _bl.GetEmployee(new Employee() { Id = id });
        }

        [HttpPost]
        public Employee? AddEmployee(Employee employee)
        {
            return _bl.AddEmployee(employee);
        }

        [HttpPut]
        public Employee? UpdateEmployee(Employee employee) 
        {
            return _bl.UpdateEmployee(employee);
        }

        [HttpDelete]
        public bool? DeleteEmployee(Guid id)
        {
            return _bl.DeleteEmployee(id);
        }

    }
}
