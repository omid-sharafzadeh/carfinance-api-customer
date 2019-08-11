using System;
using System.Net;
using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerController.SadPaths
{
    public class WhenRetrievingCustomer
    {
        private readonly Mock<ICustomerService> _mockCustomerService = new Mock<ICustomerService>();
            
        [Fact]
        public void ShouldReturnInternalServerErrorIfExceptionThrownForSingleCustomer()
        {
            _mockCustomerService.Setup(s => s.GetById(It.IsAny<string>())).ThrowsAsync(new Exception());
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);

            var result = sut.Get(It.IsAny<string>()).Result;
            var statusCode = ((ObjectResult) result).StatusCode;
                
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<Exception>(((ObjectResult) result).Value);
            Assert.Equal((int) HttpStatusCode.InternalServerError, statusCode);
        }
        
        [Fact]
        public void ShouldReturnInternalServerErrorIfExceptionThrownForMultipleCustomers()
        {
            _mockCustomerService.Setup(s => s.GetAll()).ThrowsAsync(new Exception());
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);

            var result = sut.Get().Result;
            var statusCode = ((ObjectResult) result).StatusCode;
                
            Assert.IsType<ObjectResult>(result);
            Assert.IsType<Exception>(((ObjectResult) result).Value);
            Assert.Equal((int) HttpStatusCode.InternalServerError, statusCode);
        }
            
        [Fact]
        public void ShouldReturnBadRequestIfModelStateIsInvalid()
        {
            _mockCustomerService.Setup(s => s.GetById(It.IsAny<string>()));
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            sut.ModelState.AddModelError(string.Empty, string.Empty);

            var result = sut.Get(It.IsAny<string>()).Result;
                
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void ShouldReturnNotFoundIfNoCustomerFound()
        {
            const string nonExistingCustomerId = "123";
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);

            var result = sut.Get(nonExistingCustomerId).Result;

            Assert.Equal(nonExistingCustomerId, ((NotFoundObjectResult) result).Value);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}