using EMI_API.Commons.DTOs.Identity;
using EMI_API.Commons.Enums;
using FluentValidation;

namespace EMI_API.Commons.Validators
{
    public  class EditClaimValidator : AbstractValidator<EditClaimDTO>
    {
        public EditClaimValidator() {

            RuleFor(x => x.Email).NotEmpty().WithMessage(Messages.RequiredProperty)
               .MaximumLength(256).WithMessage(Messages.MaximumLenght)
               .EmailAddress().WithMessage(Messages.Email);
        }
    }
   
}
