using System;
using System.Net;
using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerController.SadPaths
{
    public class WhenCreatingCustomer
    {
        private const string ValidEmail = "test@test.com";
        private const string ValidFirstName = "John";
        private const string ValidSurname = "Clarkin";
        private readonly Models.Customer _validCustomer = new Models.Customer(ValidEmail, ValidFirstName, ValidSurname);
        private readonly Mock<ICustomerService> _mockCustomerService = new Mock<ICustomerService>();
            
        [Fact]
        public void ShouldReturnServerErrorIfExceptionThrown()
        {
            _mockCustomerService.Setup(s => s.Add(_validCustomer)).ThrowsAsync(new Exception());
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);

            var result = sut.Post(_validCustomer).Result;
            var statusCode = ((ObjectResult) result).StatusCode;
                
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<Exception>(((ObjectResult) result).Value);
            Assert.Equal((int) HttpStatusCode.InternalServerError, statusCode);
        }
            
        [Fact]
        public void ShouldReturnBadRequestIfModelStateIsInvalid()
        {
            _mockCustomerService.Setup(s => s.Add(_validCustomer));
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            sut.ModelState.AddModelError(string.Empty, string.Empty);

            var result = sut.Post(_validCustomer).Result;
                
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}