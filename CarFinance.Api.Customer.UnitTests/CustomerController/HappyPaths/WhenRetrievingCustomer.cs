using System.Threading.Tasks;
using CarFinance.Api.Customer.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerController.HappyPaths
{
    public class WhenRetrievingCustomer
    {
        private readonly Mock<ICustomerService> _mockCustomerService = new Mock<ICustomerService>();
        
        [Fact]
        public async Task ShouldCallCustomerServiceToGetCustomerById()
        {
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            await sut.Get(It.IsAny<string>());
            
            _mockCustomerService.Verify(s => s.GetById(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnOkObjectResult()
        {
            var dummyCustomerId = It.IsAny<string>();
            _mockCustomerService.Setup(s => s.GetById(dummyCustomerId))
                .ReturnsAsync(new Models.Customer(string.Empty));
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            var result = sut.Get(dummyCustomerId).Result;

            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void ShouldReturnTheSameCustomer()
        {
            var customerId = It.IsAny<string>(); 
            const string validEmail = "test@test.com";
            var validCustomer = new Models.Customer(validEmail);
            _mockCustomerService.Setup(s => s.GetById(customerId)).ReturnsAsync(validCustomer);
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            var result = sut.Get(customerId).Result;

            var customer = (Models.Customer)((OkObjectResult) result).Value;

            Assert.Equal(validEmail, customer.Email);
        }
    }
}