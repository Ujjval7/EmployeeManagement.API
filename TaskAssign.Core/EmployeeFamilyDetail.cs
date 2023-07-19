using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssign.Core
{
    public class EmployeeFamilyDetail
    {
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public Guid RelationId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Relation { get; set; } = string.Empty;
    }
}
