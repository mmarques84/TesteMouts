using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TesteTecnicoMarcus.Api.Controllers;
using TesteTecnicoMarcus.Application.DTOs.Employees;
using TesteTecnicoMarcus.Application.Services;
using TesteTecnicoMarcus.Domain.Enums;
using TesteTecnicoMarcus.Domain.Interfaces;
using Xunit;

namespace TesteTecnicoMarcus.Tests.ControllerTest
{
    public class EmployeeControllerTests
    {
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            // 🔹 Mock dos repositórios
            var employeeRepoMock = new Mock<IEmployeeRepository>();
            var jobTitleRepoMock = new Mock<IJobTitleRepository>();

            // 🔹 Configuração mínima para não quebrar
            employeeRepoMock
                .Setup(r => r.ExistsByDocNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            employeeRepoMock
                .Setup(r => r.ExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // 🔹 Service REAL (mas com dependências fake)
            var service = new EmployeeService(
                employeeRepoMock.Object,
                jobTitleRepoMock.Object
            );

            // 🔹 Controller REAL
            _controller = new EmployeeController(service);

            // 🔹 Usuário autenticado fake
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, ERole.Admin.ToString())
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Create_Should_Return_Ok()
        {
            var dto = new EmployeeCreateDto
            {
                FirstName = "Marcus",
                LastName = "Teste",
                Email = "teste@teste.com",
                DocNumber = "123456",
                BirthDate = DateTime.UtcNow.AddYears(-30),
                Password = "123456",
                Role = ERole.Admin
            };

            var result = await _controller.Create(dto);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
