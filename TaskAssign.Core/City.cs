using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssign.Core
{
    public class City
    {
        public Guid? Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public Guid StateId { get; set; }
        public string State { get; set; } = string.Empty;
    }
}
