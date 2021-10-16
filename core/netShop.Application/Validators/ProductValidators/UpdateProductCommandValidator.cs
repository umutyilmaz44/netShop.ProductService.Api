using FluentValidation;
using netShop.Application.Features.Commands.ProductCommands;

namespace netShop.Application.Validators.ProductValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("GPD-E-PId-001")
                .NotEmpty().WithErrorCode("GPD-E-PId-002"); //.WithMessage("Please specify a valid product id.");
            RuleFor(x => x.ProductCode)
                .NotNull().WithErrorCode("GPD-E-PC-003")
                .NotEmpty().WithErrorCode("GPD-E-PC-004"); //.WithMessage("Please specify a valid product code.");
            RuleFor(x => x.ProductName)
                .NotNull().WithErrorCode("GPD-E-PN-005")
                .NotEmpty().WithErrorCode("GPD-E-PN-006")
                .Length(3, 250).WithErrorCode("GPD-E-PN-007"); //.WithMessage("Please specify a product name.");
        }
    }
}