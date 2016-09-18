using System.Threading.Tasks;
using CustomServiceRegistration.Controllers;
using CustomServiceRegistration.Domain.Infrastructure.Contracts;
using CustomServiceRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CustomServiceRegistration.Tests.UnitTests
{
    public class UserControllerTest
    {
        [Fact]
        public async Task RegisterUser_ReturnsRightStatusCode_WhenUserCreated()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var controller = new UserController(mockRepo.Object);

            // Act
            var result = await controller.Register(new RegistrationModel()
            {
                Password = "123456",
                ConfirmPassword = "123456",
                FirstName = "first name",
                SecondName = "second name",
                UserName = "username",
                Age = 22,
                Email = "someemail@gmail.com",
                CountryName = "Ukraine"
            });
            // Assert
            var value = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, value.StatusCode);
        }
    }
}
