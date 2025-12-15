using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoMarcus.Domain.Entities
{
    public class EmployeeAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Street { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!; 
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;
    }

}
