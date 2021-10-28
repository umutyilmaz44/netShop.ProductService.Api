using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.ProductCommands;

namespace NetShop.ProductService.Application.Validators.ProductValidators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("DPC-E-PId-001")
                .NotEmpty().WithErrorCode("DPC-E-PId-002"); //.WithMessage("Please specify a valid product id.");
        }
    }
}