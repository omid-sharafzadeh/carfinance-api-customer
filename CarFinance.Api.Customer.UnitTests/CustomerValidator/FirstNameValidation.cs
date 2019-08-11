using System;
using FluentValidation.TestHelper;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests.CustomerValidator
{
    public class FirstNameValidation
    {
        private readonly Validators.CustomerValidator _validator = new Validators.CustomerValidator();
        private const string Email = "email@test.com";
        
        [Fact]
        public void ShouldNotBeEmpty()
        {
            var firstName = string.Empty;
            var customer = new Models.Customer(Email, firstName, "Clarkin", "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.FirstName, customer);
        }
        
        [Fact]
        public void ShouldNotBeNull()
        {
            string firstName = null;
            var customer = new Models.Customer(Email, firstName, "Clarkin", "Password123");

            var result = _validator.TestValidate(customer);
                
            result.ShouldHaveError()
                .WithErrorCode("NotNullValidator");
        }
            
        [Fact]
        public void ShouldNotBeMoreThan80Characters()
        {
            var firstName = new string('2', 81);
            var customer = new Models.Customer(Email, firstName, "Clarkin", "Password123");

            _validator.ShouldHaveValidationErrorFor(c => c.FirstName, customer);
        }
    }
}