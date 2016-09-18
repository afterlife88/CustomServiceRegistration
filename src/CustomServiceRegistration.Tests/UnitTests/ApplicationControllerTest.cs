using System.Threading.Tasks;
using CustomServiceRegistration.Controllers;
using CustomServiceRegistration.Models;
using CustomServiceRegistration.Services.Applications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CustomServiceRegistration.Tests.UnitTests
{
    public class ApplicationControllerTest
    {
        [Fact]
        public async Task AddApplication_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IApplicationService>();
            var controller = new ApplicationController(mockService.Object);

            // Act
            var result = await controller.AddApplication(new ApplicationModel() { Name = null });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
