using FluentValidation;
using netShop.Application.Features.Commands.BrandCommands;

namespace netShop.Application.Validators.BrandValidators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("UBD-E-BId-001")
                .NotEmpty().WithErrorCode("UBD-E-BId-002"); 
            RuleFor(x => x.BrandName)
                .NotNull().WithErrorCode("GBD-E-BN-003")
                .NotEmpty().WithErrorCode("GBD-E-BN-004")
                .Length(3, 250).WithErrorCode("GBD-E-BN-005");
        }
    }
}