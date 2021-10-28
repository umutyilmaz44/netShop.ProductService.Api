using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.BrandModelCommands;

namespace NetShop.ProductService.Application.Validators.BrandModelValidators
{
    public class UpdateBrandModelCommandValidator : AbstractValidator<UpdateBrandModelCommand>
    {
        public UpdateBrandModelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("UBMC-E-BMId-001")
                .NotEmpty().WithErrorCode("UBMC-E-BMId-002"); //.WithMessage("Please specify a valid BrandModel id.");
            RuleFor(x => x.BrandId)
                .NotNull().WithErrorCode("UBMC-E-BId-003")
                .NotEmpty().WithErrorCode("UBMC-E-BId-004"); //.WithMessage("Please specify a valid BrandModel code.");
            RuleFor(x => x.ModelName)
                .NotNull().WithErrorCode("UBMC-E-MN-005")
                .NotEmpty().WithErrorCode("UBMC-E-MN-006")
                .Length(3, 250).WithErrorCode("UBMC-E-MN-007"); //.WithMessage("Please specify a BrandModel name.");
        }
    }
}