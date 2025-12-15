using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteTecnicoMarcus.Api.Controllers;
using TesteTecnicoMarcus.Application.DTOs.Auth;
using TesteTecnicoMarcus.Application.Services.Interfaces;
using Xunit;

namespace TesteTecnicoMarcus.Tests.ControllerTest
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_Should_Return_Ok_When_Valid_Credentials()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "teste@teste.com",
                Password = "123456"
            };

            _authServiceMock
                .Setup(s => s.LoginAsync(dto))
                .ReturnsAsync("fake-jwt-token");

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Login_Should_Return_Unauthorized_When_Exception()
        {
            // Arrange
            var dto = new LoginDto
            {
                Email = "teste@teste.com",
                Password = "errada"
            };

            _authServiceMock
                .Setup(s => s.LoginAsync(dto))
                .ThrowsAsync(new Exception("Credenciais inválidas"));

            // Act
            var result = await _controller.Login(dto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
