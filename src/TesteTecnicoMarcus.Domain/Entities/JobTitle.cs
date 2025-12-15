using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoMarcus.Domain.Enums;

namespace TesteTecnicoMarcus.Domain.Entities
{
    public class JobTitle
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = default!; 

        public EDepartment Department { get; set; } 

        public bool IsActive { get; set; } = true;  


    }

}
