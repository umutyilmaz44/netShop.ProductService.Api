using FluentValidation;
using netShop.Application.Features.Commands.SupplierCommands;

namespace netShop.Application.Validators.SupplierValidators
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.SupplierName)
                .NotNull().WithErrorCode("CSC-E-SN-001")
                .NotEmpty().WithErrorCode("CSC-E-SN-002"); 
            RuleFor(x => x.Website)
                .NotNull().WithErrorCode("CSC-E-WS-003")
                .NotEmpty().WithErrorCode("CSC-E-WS-004");
            RuleFor(x => x.Email)
                .NotNull().WithErrorCode("CSC-E-EM-003")
                .NotEmpty().WithErrorCode("CSC-E-EM-004");
            RuleFor(x => x.Phone)
                .NotNull().WithErrorCode("CSC-E-PH-003")
                .NotEmpty().WithErrorCode("CSC-E-PH-004");     
        }
    }
}