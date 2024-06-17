using FluentValidation;
using Task.Api.DTO;

namespace Task.Api.Validators
{
    public class UserValidator : AbstractValidator<AddUserDTO>
    {
        public UserValidator()
        {
            ApplyValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address");
        }
    }

}

