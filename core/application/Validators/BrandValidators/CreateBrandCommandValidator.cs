using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.BrandCommands;

namespace NetShop.ProductService.Application.Validators.BrandValidators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(x => x.BrandName)
                .NotNull().WithErrorCode("CBC-E-BN-001")
                .NotEmpty().WithErrorCode("CBC-E-BN-002"); //.WithMessage("Please specify a valid Brand code.");
        }
    }
}