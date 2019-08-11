using System;
using System.Net;
using CarFinance.Api.Customer.Controllers;
using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
// ReSharper disable ClassNeverInstantiated.Global

namespace CarFinance.Api.Customer.UnitTests
{
    public class CustomerControllerTests
    {
        private const string ValidEmail = "test@test.com";
        private static readonly Models.Customer ValidCustomer = new Models.Customer(ValidEmail);

        public class HappyPaths
        {
            private static readonly Mock<ICustomerService> MockCustomerService = new Mock<ICustomerService>();
            
            public class WhenCreatingCustomer
            {
                [Fact]
                public void ShouldCallDatabaseToInsertCustomer()
                {
                    var sut = new CustomerController(MockCustomerService.Object);
                    
                    sut.Post(ValidCustomer);
                    
                    MockCustomerService.Verify(s => s.Add(ValidCustomer), Times.Once);
                }
                
                [Fact]
                public void ShouldReturnCreatedResult()
                {
                    var sut = new CustomerController(MockCustomerService.Object);

                    var result = sut.Post(ValidCustomer).Result;

                    Assert.IsType<CreatedResult>(result);
                }
                
                [Fact]
                public void ShouldReturnCustomerWithTheSameEmailPassedIntoIt()
                {
                    MockCustomerService.Setup(s => s.Add(ValidCustomer)).ReturnsAsync(ValidCustomer);
                    var sut = new CustomerController(MockCustomerService.Object);

                    var result = sut.Post(ValidCustomer).Result;

                    var customer = (Models.Customer)((CreatedResult) result).Value;

                    Assert.Equal(ValidEmail, customer.Email);
                }
            }
        }

        public class SadPaths
        {
            private static readonly Mock<ICustomerService> MockCustomerService = new Mock<ICustomerService>();
            
            public class WhenCreatingCustomer
            {
                [Fact]
                public void ShouldReturnServerErrorIfDbThrowsException()
                {
                    MockCustomerService.Setup(s => s.Add(ValidCustomer)).ThrowsAsync(new Exception());
                    var sut = new CustomerController(MockCustomerService.Object);

                    var result = sut.Post(ValidCustomer).Result;
                    var statusCode = ((StatusCodeResult) result).StatusCode;
                    
                    Assert.IsType<StatusCodeResult>(result);
                    Assert.Equal((int) HttpStatusCode.InternalServerError, statusCode);
                }
                
                [Fact]
                public void ShouldReturnBadRequestIfModelStateIsInvalid()
                {
                    MockCustomerService.Setup(s => s.Add(ValidCustomer));
                    var sut = new CustomerController(MockCustomerService.Object);
                    sut.ModelState.AddModelError(string.Empty, string.Empty);

                    var result = sut.Post(ValidCustomer).Result;
                    
                    Assert.IsType<BadRequestObjectResult>(result);
                }
            }
        }
    }
}