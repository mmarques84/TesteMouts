using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteTecnicoMarcus.Application.DTOs.Auth
{
    public class ResetPasswordDto
    {
        public Guid EmployeeId { get; set; }
        public string NewPassword { get; set; }
    }

}
