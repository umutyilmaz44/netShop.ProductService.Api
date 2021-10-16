using FluentValidation;
using netShop.Application.Features.Commands;

namespace netShop.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductCode)
                .NotNull().WithErrorCode("CPC-E-PC-001")
                .NotEmpty().WithErrorCode("CPC-E-PC-002"); //.WithMessage("Please specify a valid product code.");
            RuleFor(x => x.ProductName)
                .NotNull().WithErrorCode("CPC-E-PN-003")
                .NotEmpty().WithErrorCode("CPC-E-PN-004")
                .Length(3, 250).WithErrorCode("CPC-E-PN-005"); //.WithMessage("Please specify a product name.");
        }
    }
}