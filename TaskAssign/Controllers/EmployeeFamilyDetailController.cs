using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeFamilyDetailController : ControllerBase
    {
        private readonly EmployeeFamilyDetailBL _bl;

        public EmployeeFamilyDetailController(EmployeeFamilyDetailBL bl) 
        {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<EmployeeFamilyDetail> GetEmployees()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public EmployeeFamilyDetail? GetEmployeeFamily(Guid id)
        {
            return _bl.GetEmployeeFamily(new EmployeeFamilyDetail() { Id = id });
        }

        [HttpPost]
        public EmployeeFamilyDetail? AddEmployeeFamily(EmployeeFamilyDetail employeeFamily)
        {
            return _bl.AddEmployeeFamily(employeeFamily);
        }

        [HttpPut]
        public EmployeeFamilyDetail? UpdateEmployeeFamily(EmployeeFamilyDetail employeeFamily)
        {
            return _bl.UpdateFamilyFamily(employeeFamily);
        }

        [HttpDelete]
        public bool? DeleteEmployeeFamily(Guid id)
        {
            return _bl.DeleteEmployeeFamily(id);
        }

    }
}
