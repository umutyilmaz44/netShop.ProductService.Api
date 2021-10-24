using FluentValidation;
using netShop.Application.Features.Commands.SupplierCommands;

namespace netShop.Application.Validators.SupplierValidators
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("USD-E-PId-001")
                .NotEmpty().WithErrorCode("USD-E-PId-002");
            RuleFor(x => x.SupplierName)
                .NotNull().WithErrorCode("USD-E-PC-003")
                .NotEmpty().WithErrorCode("USD-E-PC-004"); 
            RuleFor(x => x.Email)
                .NotNull().WithErrorCode("USD-E-EM-005")
                .NotEmpty().WithErrorCode("USD-E-EM-006");     
            RuleFor(x => x.Website)
                .NotNull().WithErrorCode("USD-E-WS-007")
                .NotEmpty().WithErrorCode("USD-E-WS-008");            
        }
    }
}