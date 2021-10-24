using FluentValidation;
using netShop.Application.Features.Queries.SupplierQueries;

namespace netShop.Application.Validators.SupplierValidators
{
    public class FindSuppliersQueryValidator : AbstractValidator<FindSuppliersQuery>
    {
        public FindSuppliersQueryValidator()
        {
            // RuleFor(x => x.SupplierCode)
            //     .NotNull().WithErrorCode("PQ-E-PC-001")
            //     .NotEmpty().WithErrorCode("PQ-E-PC-002"); //.WithMessage("Please specify a valid Supplier code.");
            // RuleFor(x => x.SupplierName)
            //     .NotNull().WithErrorCode("PQ-E-PN-003")
            //     .NotEmpty().WithErrorCode("PQ-E-PN-004")
            //     .Length(3, 250).WithErrorCode("PQ-E-PN-005"); //.WithMessage("Please specify a Supplier name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}