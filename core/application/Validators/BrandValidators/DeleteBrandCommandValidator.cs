using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.BrandCommands;

namespace NetShop.ProductService.Application.Validators.BrandValidators
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("DBC-E-BId-001")
                .NotEmpty().WithErrorCode("DBC-E-BId-002"); //.WithMessage("Please specify a valid Brand id.");
        }
    }
}