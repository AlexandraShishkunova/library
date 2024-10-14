using libraryWeb;
using libraryWeb.Controllers;
using libraryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly LibraryContext _context;


        [Fact]
        public async Task Login_User_Failure()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Username = "wronguser",
                Password = "wrongpassword"
            };

            // Act
            var result = await _controller.Login(loginDto) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }
    }
}
