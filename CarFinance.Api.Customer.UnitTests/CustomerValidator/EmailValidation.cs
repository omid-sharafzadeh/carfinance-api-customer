using FluentValidation.TestHelper;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerValidator
{
    public class EmailValidationTests
    {
        private readonly Validators.CustomerValidator _validator = new Validators.CustomerValidator();

        [Fact]
        public void ShouldNotBeEmpty()
        {
            var email = string.Empty;
            var customer = new Models.Customer(email, "John", "Clarkin", "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.Email, customer);
        }
            
        [Fact]
        public void ShouldNotBeNull()
        {
            string email = null;
            var customer = new Models.Customer(email, "John", "Clarkin", "Password123");

            var result = _validator.TestValidate(customer);
                
            result.ShouldHaveError()
                .WithErrorCode("NotNullValidator");
        }
            
        [Fact]
        public void ShouldHaveErrorsIfInvalidEmail()
        {
            const string email = "test.com";
            var customer = new Models.Customer(email, "John", "Clarkin", "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.Email, customer);
        }
            
        [Fact]
        public void ShouldNotHaveErrorsIfValidEmail()
        {
            const string email = "test@test.com";
            var customer = new Models.Customer(email, "John", "Clarkin", "Password123");

            _validator.ShouldNotHaveValidationErrorFor(c => c.Email, customer);
        }
    }
}