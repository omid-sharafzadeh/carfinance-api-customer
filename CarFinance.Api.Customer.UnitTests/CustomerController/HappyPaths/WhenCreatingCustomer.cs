using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerController.HappyPaths
{
    public class WhenCreatingCustomer
        {
            private const string ValidEmail = "test@test.com";
            private const string ValidFirstName = "John";
            private const string ValidSurname = "Clarkin";
            private const string ValidPassword = "Password123";
            private readonly Models.Customer _validCustomer = new Models.Customer(ValidEmail, ValidFirstName, ValidSurname, ValidPassword);
            private readonly Mock<ICustomerService> _mockCustomerService = new Mock<ICustomerService>();
                
            [Fact]
            public void ShouldCallCustomerServiceToAddCustomer()
            {
                var sut = new Controllers.CustomerController(_mockCustomerService.Object);
                    
                sut.Post(_validCustomer).Wait();
                    
                _mockCustomerService.Verify(s => s.Add(_validCustomer), Times.Once);
            }
                
            [Fact]
            public void ShouldReturnCreatedResult()
            {
                var sut = new Controllers.CustomerController(_mockCustomerService.Object);

                var result = sut.Post(_validCustomer).Result;

                Assert.IsType<CreatedResult>(result);
            }
                
            [Fact]
            public void ShouldReturnCustomerWithTheSameEmailPassedIntoIt()
            {
                _mockCustomerService.Setup(s => s.Add(_validCustomer)).ReturnsAsync(_validCustomer);
                var sut = new Controllers.CustomerController(_mockCustomerService.Object);

                var result = sut.Post(_validCustomer).Result;

                var customer = (Models.Customer)((CreatedResult) result).Value;

                Assert.Equal(ValidEmail, customer.Email);
            }
        }
    }