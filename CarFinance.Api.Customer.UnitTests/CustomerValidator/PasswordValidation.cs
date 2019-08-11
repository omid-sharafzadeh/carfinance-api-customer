using FluentValidation.TestHelper;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerValidator
{
    public class PasswordValidation
    {
        private readonly Validators.CustomerValidator _validator = new Validators.CustomerValidator();
        private const string Email = "email@test.com";
        private const string FirstName = "John";
        private const string Surname = "John";
        
        [Fact]
        public void ShouldNotBeEmpty()
        {
            var password = string.Empty;
            var customer = new Models.Customer(Email, FirstName, Surname, password);

            _validator.ShouldHaveValidationErrorFor(c => c.Password, customer);
        }
        
        [Fact]
        public void ShouldNotBeNull()
        {
            string password = null;
            var customer = new Models.Customer(Email, FirstName, Surname, password);

            var result = _validator.TestValidate(customer);
                
            result.ShouldHaveError()
                .WithErrorCode("NotNullValidator");
        }
            
        [Fact]
        public void ShouldNotBeMoreThan50Characters()
        {
            var password = new string('2', 51);
            var customer = new Models.Customer(Email, FirstName, Surname, password);

            _validator.ShouldHaveValidationErrorFor(c => c.Password, customer);
        }
        
        [Fact]
        public void ShouldNotBeLessThan6Characters()
        {
            var password = new string('2', 7);
            var customer = new Models.Customer(Email, FirstName, Surname, password);

            _validator.ShouldNotHaveValidationErrorFor(c => c.Password, customer);
        }
    }
}