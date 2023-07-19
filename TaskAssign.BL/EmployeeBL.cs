using System.Reflection.Emit;
using TaskAssign.Core;
using TaskAssign.DAL;

namespace TaskAssign.BL
{
    public class EmployeeBL
    {
        private readonly EmployeeRepository _repository;

        public EmployeeBL(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<Employee> GetAll()
        {
            return _repository.GetAll();
        }

        public Employee? AddEmployee(Employee employee)
        {

            var existingemployee = GetEmployee(new Employee() { Email = employee.Email });
            if (existingemployee != null)
            {
                throw new Exception("Employee Already Exist");
            }

            var existingecode = GetEmployee(new Employee() { EmpCode = employee.EmpCode });
            if (existingecode != null)
            {
                throw new Exception("Please Enter Different Employee Code");
            }

            var id = _repository.AddEmployee(employee);
            if (id != null)
            {
                employee.Id = id;
            }
            return employee;
        }

        public Employee? GetEmployee(Employee employee)
        {
            return _repository.GetEmployee(employee);
        }

        public Employee? UpdateEmployee(Employee employee)
        {
            var incorrectId = GetEmployee(new Employee() { Id = employee.Id });
            {
                if (incorrectId == null)
                {
                    throw new Exception("Please Enter Correct Id");
                }
            }

            //var existingemployee = GetEmployee(new Employee() { Email = employee.Email });
            //if (existingemployee != null)
            //{
            //    throw new Exception("Email Already Exists");
            //}

            return _repository.UpdateEmployee(employee);
        }

        public bool? DeleteEmployee(Guid id)
        {
            return _repository.DeleteEmployee(id);
        }

    }
}