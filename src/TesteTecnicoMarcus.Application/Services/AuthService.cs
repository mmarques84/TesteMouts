using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteTecnicoMarcus.Application.DTOs.Auth;
using TesteTecnicoMarcus.Application.Services.Interfaces;
using TesteTecnicoMarcus.Domain.Entities;
using TesteTecnicoMarcus.Domain.Enums;
using TesteTecnicoMarcus.Domain.Interfaces;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _repository;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<Employee> _hasher = new();

    public AuthService(IEmployeeRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = (await _repository.GetByEmailAsync(dto.Email));
         

        if (user == null)
            throw new Exception("Usuário não encontrado.");
        var hasher = new PasswordHasher<Employee>();
   
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result != PasswordVerificationResult.Success)
            throw new Exception("Senha incorreta.");

        return GenerateJwtToken(user);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var employee = await _repository.GetByIdAsync(dto.EmployeeId);

        if (employee == null)
            throw new Exception("Usuário não encontrado.");

        var hasher = new PasswordHasher<Employee>();
        employee.PasswordHash = hasher.HashPassword(employee, dto.NewPassword);

        await _repository.UpdateAsync(employee);
        await _repository.SaveChangesAsync();
    }
    private string GenerateJwtToken(Employee user)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
             new Claim("roleId", ((int)user.Role).ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
