using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.BrandModelCommands;

namespace NetShop.ProductService.Application.Validators.BrandModelValidators
{
    public class DeleteBrandModelCommandValidator : AbstractValidator<DeleteBrandModelCommand>
    {
        public DeleteBrandModelCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("DBMC-E-BMId-001")
                .NotEmpty().WithErrorCode("DBMC-E-BMId-002"); //.WithMessage("Please specify a valid BrandModel id.");
        }
    }
}