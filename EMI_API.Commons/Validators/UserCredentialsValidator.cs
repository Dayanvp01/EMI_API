using EMI_API.Commons.DTOs.Identity;
using EMI_API.Commons.Enums;
using FluentValidation;


namespace EMI_API.Commons.Validators
{
    public class UserCredentialsValidator : AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialsValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(Messages.RequiredProperty)
               .MaximumLength(256).WithMessage(Messages.MaximumLenght)
               .EmailAddress().WithMessage(Messages.Email);

            RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.RequiredProperty);
        }
    }
}
