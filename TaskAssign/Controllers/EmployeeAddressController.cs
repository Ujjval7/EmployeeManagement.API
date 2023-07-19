using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAssign.BL;
using TaskAssign.Core;

namespace TaskAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAddressController : ControllerBase
    {
        private readonly EmployeeAddressBL _bl;

        public EmployeeAddressController(EmployeeAddressBL bL)
        {
            _bl = bL;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<EmployeeAddress> GetAll()
        {
            return _bl.GetAll();
        }

        [HttpGet]
        public EmployeeAddress? GetEmployeeAddress(Guid id)
        {
            return _bl.GetEmployeeAddress(new EmployeeAddress() { Id = id });
        }

        [HttpPost]
        public EmployeeAddress? AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            return _bl.AddEmployeeaddress(employeeAddress);
        }

        [HttpPut]
        public EmployeeAddress? UpdateEmployeeAddree(EmployeeAddress employeeAddress)
        {
            return _bl.UpdateEmployeeAddress(employeeAddress);
        }

        [HttpDelete]
        public bool? DeleteEmployeeAddress(Guid id)
        {
            return _bl.DeleteEmployeeAddress(id);
        }

    }
}
