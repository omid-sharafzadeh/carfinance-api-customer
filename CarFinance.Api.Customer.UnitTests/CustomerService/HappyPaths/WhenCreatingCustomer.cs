using CarFinance.Api.Customer.Data;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerService.HappyPaths
{
    public class WhenCreatingCustomer
    {
        private const string NewCustomerFirstName = "John";
        private const string NewCustomerSurname = "Clarkin";
        private const string NewCustomerEmail = "test@test.com";
        private const string NewCustomerPassword = "Password123";
        
        [Fact]
        public void ShouldCallDatabaseToInsertCustomer()
        {
            var newCustomer = new Models.Customer(NewCustomerEmail, NewCustomerFirstName, NewCustomerSurname, NewCustomerPassword);
            var mockDatabase = new Mock<ICustomerDb>();
            var sut = new Services.CustomerService(mockDatabase.Object);
                    
            sut.Add(newCustomer);
                    
            mockDatabase.Verify(db => db.Insert(newCustomer), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnCustomerWithTheSameEmailPassedIntoIt()
        {
            var newCustomer = new Models.Customer(NewCustomerEmail, NewCustomerFirstName, NewCustomerSurname, NewCustomerPassword);
            var mockDatabase = new Mock<ICustomerDb>();
            mockDatabase.Setup(db => db.Insert(newCustomer)).ReturnsAsync(newCustomer);
            var sut = new Services.CustomerService(mockDatabase.Object);
            
            var actualNewCustomer = sut.Add(newCustomer).Result;

            Assert.Equal(NewCustomerEmail, actualNewCustomer.Email);
        }
    }
}