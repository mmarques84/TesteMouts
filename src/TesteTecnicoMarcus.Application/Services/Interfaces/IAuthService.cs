using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoMarcus.Application.DTOs.Auth;

namespace TesteTecnicoMarcus.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
    }

}
