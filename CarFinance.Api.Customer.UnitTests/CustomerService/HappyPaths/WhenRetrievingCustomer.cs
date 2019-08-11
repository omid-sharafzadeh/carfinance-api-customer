using CarFinance.Api.Customer.Data;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerService.HappyPaths
{
    public class WhenRetrievingCustomer
    {
        [Fact]
        public void ShouldCallDatabaseToGetCustomerById()
        {
            var mockDatabase = new Mock<ICustomerDb>();
            var sut = new Services.CustomerService(mockDatabase.Object);
                    
            sut.GetById(It.IsAny<string>());
                    
            mockDatabase.Verify(db => db.GetById(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public void ShouldCallDatabaseToGetAllCustomers()
        {
            var mockDatabase = new Mock<ICustomerDb>();
            var sut = new Services.CustomerService(mockDatabase.Object);
                    
            sut.GetAll();
                    
            mockDatabase.Verify(db => db.GetAll(), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnTheSameCustomerWithTheSameEmail()
        {
            const string newCustomerEmail = "test@test.com";
            const string newCustomerFirstName = "John";
            const string newCustomerSurname = "Clarkin";
            const string newCustomerPassword = "Password123";
            var existingCustomer = new Models.Customer(newCustomerEmail, newCustomerFirstName, newCustomerSurname, newCustomerPassword);
            var mockDatabase = new Mock<ICustomerDb>();
            mockDatabase.Setup(db => db.GetById(It.IsAny<string>())).ReturnsAsync(existingCustomer);
            var sut = new Services.CustomerService(mockDatabase.Object);
            
            var actualNewCustomer = sut.GetById(It.IsAny<string>()).Result;

            Assert.Equal(newCustomerEmail, actualNewCustomer.Email);
        }
    }
}