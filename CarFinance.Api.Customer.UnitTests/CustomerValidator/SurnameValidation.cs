using FluentValidation.TestHelper;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerValidator
{
    public class SurnameValidation
    {
        private readonly Validators.CustomerValidator _validator = new Validators.CustomerValidator();
        private const string Email = "email@test.com";
        private const string FirstName = "John";
        
        [Fact]
        public void ShouldNotBeEmpty()
        {
            var surname = string.Empty;
            var customer = new Models.Customer(Email, FirstName, surname, "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.Surname, customer);
        }
        
        [Fact]
        public void ShouldNotBeNull()
        {
            string surname = null;
            var customer = new Models.Customer(Email, FirstName, surname, "Password123");

            var result = _validator.TestValidate(customer);
                
            result.ShouldHaveError()
                .WithErrorCode("NotNullValidator");
        }
            
        [Fact]
        public void ShouldNotBeMoreThan80Characters()
        {
            var surname = new string('2', 81);
            var customer = new Models.Customer(Email, FirstName, surname, "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.Surname, customer);
        }
    }
}