using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.BrandModelCommands;

namespace NetShop.ProductService.Application.Validators.BrandModelValidators
{
    public class CreateBrandModelCommandValidator : AbstractValidator<CreateBrandModelCommand>
    {
        public CreateBrandModelCommandValidator()
        {
            RuleFor(x => x.BrandId)
                .NotNull().WithErrorCode("CBMC-E-BId-001")
                .NotEmpty().WithErrorCode("CBMC-E-BId-002"); //.WithMessage("Please specify a valid BrandModel code.");
            RuleFor(x => x.ModelName)
                .NotNull().WithErrorCode("CBMC-E-MN-003")
                .NotEmpty().WithErrorCode("CBMC-E-MN-004")
                .Length(3, 250).WithErrorCode("CBMC-E-MN-005"); //.WithMessage("Please specify a BrandModel name.");
        }
    }
}