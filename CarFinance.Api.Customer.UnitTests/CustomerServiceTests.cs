using CarFinance.Api.Customer.Data;
using CarFinance.Api.Customer.Services;
using Moq;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void ShouldCallDatabaseToInsertCustomer()
        {
            var newCustomer = new Models.Customer("test@test.com");
            var mockDatabase = new Mock<ICustomerDb>();
            var sut = new CustomerService(mockDatabase.Object);
                    
            sut.Add(newCustomer);
                    
            mockDatabase.Verify(db => db.Insert(newCustomer), Times.Once);
        }
        
        [Fact]
        public void ShouldReturnCustomerWithTheSameEmailPassedIntoIt()
        {
            const string newCustomerEmail = "test@test.com";
            var newCustomer = new Models.Customer(newCustomerEmail);
            var mockDatabase = new Mock<ICustomerDb>();
            mockDatabase.Setup(db => db.Insert(newCustomer)).ReturnsAsync(newCustomer);
            var sut = new CustomerService(mockDatabase.Object);
            
            var actualNewCustomer = sut.Add(newCustomer).Result;

            Assert.Equal(newCustomerEmail, actualNewCustomer.Email);
        }
    }
}