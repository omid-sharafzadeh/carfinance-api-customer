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

            RuleFor(customer => customer.FirstName).NotEmpty();
            RuleFor(customer => customer.FirstName).NotNull();
            RuleFor(customer => customer.FirstName).MaximumLength(80);
            
            RuleFor(customer => customer.Surname).NotEmpty();
            RuleFor(customer => customer.Surname).NotNull();
            RuleFor(customer => customer.Surname).MaximumLength(80);
            
            RuleFor(customer => customer.Password).NotEmpty();
            RuleFor(customer => customer.Password).NotNull();
            RuleFor(customer => customer.Password).MaximumLength(50);
            RuleFor(customer => customer.Password).MinimumLength(6);
        }
    }
}