using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class EmployeeFamilyDetailBL
    {
        private readonly EmployeeFamilyDetailRepository _repository;

        public EmployeeFamilyDetailBL(EmployeeFamilyDetailRepository repository) 
        {
            _repository = repository;
        }

        public List<EmployeeFamilyDetail> GetAll()
        {
            return _repository.GetAll();
        }

        public EmployeeFamilyDetail? AddEmployeeFamily(EmployeeFamilyDetail employeeFamily)
        {
            var id = _repository.AddEmployeeFamily(employeeFamily);
            if(id != null)
            {
                employeeFamily.Id = id;
            }
            return employeeFamily;
        }

        public EmployeeFamilyDetail? GetEmployeeFamily(EmployeeFamilyDetail employeeFamily)
        {
            return _repository.GetEmployeeFamily(employeeFamily);
        }

        public EmployeeFamilyDetail? UpdateFamilyFamily(EmployeeFamilyDetail employeeFamily)
        {
            var incorrectId = GetEmployeeFamily(new EmployeeFamilyDetail() { Id = employeeFamily.Id });
            if(incorrectId == null)
            {
                throw new Exception("Please Enter Correct Id");
            }

            return _repository.UpdateEmployeeFamily(employeeFamily);
        }

        public bool? DeleteEmployeeFamily(Guid id)
        {
            return _repository.DeleteEmployeeFamily(id);
        }

    }
}
