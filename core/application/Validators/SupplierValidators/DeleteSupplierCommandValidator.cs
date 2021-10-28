using FluentValidation;
using NetShop.ProductService.Application.Features.Commands.SupplierCommands;

namespace NetShop.ProductService.Application.Validators.SupplierValidators
{
    public class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("DSC-E-SId-001")
                .NotEmpty().WithErrorCode("DSC-E-SId-002");
        }
    }
}