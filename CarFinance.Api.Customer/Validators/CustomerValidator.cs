using FluentValidation;

namespace CarFinance.Api.Customer.Validators
{
    public class CustomerValidator : AbstractValidator<Models.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Email).NotEmpty();
            RuleFor(customer => customer.Email).NotNull();
            RuleFor(customer => customer.Email).EmailAddress();
        }
    }
}