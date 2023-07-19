using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssign.Core
{
    public class EmployeeAddress
    {
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Address { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
        public Guid CityId { get; set; }
        public Guid AddressTypeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string AddressType { get; set; } = string.Empty;
    }
}
