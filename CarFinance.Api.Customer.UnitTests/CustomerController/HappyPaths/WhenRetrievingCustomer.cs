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
        public async Task ShouldCallCustomerServiceToGetAllCustomers()
        {
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            await sut.Get();
            
            _mockCustomerService.Verify(s => s.GetAll(), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnOkObjectResultForSingleCustomer()
        {
            var dummyCustomerId = It.IsAny<string>();
            _mockCustomerService.Setup(s => s.GetById(dummyCustomerId))
                .ReturnsAsync(new Models.Customer(string.Empty, string.Empty, string.Empty, string.Empty));
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            var result = sut.Get(dummyCustomerId).Result;

            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void ShouldReturnOkObjectResultForMultipleCustomers()
        {
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            var result = sut.Get().Result;

            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void ShouldReturnTheSameCustomer()
        {
            var customerId = It.IsAny<string>(); 
            const string validEmail = "test@test.com";
            const string validFirstName = "John";
            const string validSurname = "Surname";
            const string validPassword = "Password123";
            var validCustomer = new Models.Customer(validEmail, validFirstName, validSurname, validPassword);
            _mockCustomerService.Setup(s => s.GetById(customerId)).ReturnsAsync(validCustomer);
            var sut = new Controllers.CustomerController(_mockCustomerService.Object);
            
            var result = sut.Get(customerId).Result;

            var customer = (Models.Customer)((OkObjectResult) result).Value;

            Assert.Equal(validEmail, customer.Email);
        }
    }
}