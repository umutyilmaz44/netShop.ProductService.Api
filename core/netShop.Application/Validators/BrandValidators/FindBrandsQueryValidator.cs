using FluentValidation;
using netShop.Application.Features.Queries.BrandQueries;

namespace netShop.Application.Validators.BrandValidators
{
    public class FindBrandsQueryValidator : AbstractValidator<FindBrandsQuery>
    {
        public FindBrandsQueryValidator()
        {
            // RuleFor(x => x.BrandCode)
            //     .NotNull().WithErrorCode("PQ-E-PC-001")
            //     .NotEmpty().WithErrorCode("PQ-E-PC-002"); //.WithMessage("Please specify a valid Brand code.");
            // RuleFor(x => x.BrandName)
            //     .NotNull().WithErrorCode("PQ-E-PN-003")
            //     .NotEmpty().WithErrorCode("PQ-E-PN-004")
            //     .Length(3, 250).WithErrorCode("PQ-E-PN-005"); //.WithMessage("Please specify a Brand name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}