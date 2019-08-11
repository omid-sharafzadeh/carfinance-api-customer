using CarFinance.Api.Customer.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace CarFinance.Api.Customer.UnitTests
{
    public class CustomerValidatorTests
    {
        public class Email
        {
            private readonly CustomerValidator _validator = new CustomerValidator();

            [Fact]
            public void ShouldNotBeEmpty()
            {
                var email = string.Empty;
                var customer = new Models.Customer(email);

                _validator.ShouldHaveValidationErrorFor(c => c.Email, customer);
            }
            
            [Fact]
            public void ShouldNotBeNull()
            {
                string email = null;
                var customer = new Models.Customer(email);

                var result = _validator.TestValidate(customer);
                
                result.ShouldHaveError()
                    .WithErrorCode("NotNullValidator");
            }
            
            [Fact]
            public void ShouldHaveErrorsIfInvalidEmail()
            {
                var email = "test.com";
                var customer = new Models.Customer(email);

                _validator.ShouldHaveValidationErrorFor(c => c.Email, customer);
            }
            
            [Fact]
            public void ShouldNotHaveErrorsIfValidEmail()
            {
                var email = "test@test.com";
                var customer = new Models.Customer(email);

                _validator.ShouldNotHaveValidationErrorFor(c => c.Email, customer);
            }
        }
    }
}