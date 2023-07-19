using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class EmployeeAddressBL
    {
        private readonly EmployeeAddressRepository _repository;

        public EmployeeAddressBL(EmployeeAddressRepository repository) 
        {
            _repository = repository;
        }

        public List<EmployeeAddress> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public EmployeeAddress? AddEmployeeaddress(EmployeeAddress employeeAddress)
        {
            //var existingEmployee = GetEmployeeAddress(new EmployeeAddress() { EmployeeId = employeeAddress.EmployeeId });
            //if(existingEmployee != null)
            //{
            //    throw new Exception("Employee Address Already Exists");
            //}

            var id = _repository.AddEmployeeAddress(employeeAddress);
            if(id != null)
            {
                employeeAddress.Id= id;
            }
            return employeeAddress;
        }

        public EmployeeAddress? GetEmployeeAddress(EmployeeAddress employeeAddress)
        {
            return _repository.GetEmployeeAddress(employeeAddress);
        }

        public EmployeeAddress? UpdateEmployeeAddress(EmployeeAddress employeeAddress)
        {

            var incorrectId = GetEmployeeAddress(new EmployeeAddress() { Id = employeeAddress.Id });
            if(incorrectId == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpdateEmployeeAddress(employeeAddress);
        }

        public bool? DeleteEmployeeAddress(Guid id)
        {
            return _repository.DeleteEmployeeAddress(id);
        }

    }
}
