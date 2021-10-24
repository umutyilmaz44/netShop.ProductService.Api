using FluentValidation;
using netShop.Application.Features.Commands.BrandModelCommands;

namespace netShop.Application.Validators.BrandModelValidators
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