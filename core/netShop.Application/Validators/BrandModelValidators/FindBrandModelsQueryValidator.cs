using FluentValidation;
using netShop.Application.Features.Queries.BrandModelQueries;

namespace netShop.Application.Validators.BrandModelValidators
{
    public class FindBrandModelsQueryValidator : AbstractValidator<FindBrandModelsQuery>
    {
        public FindBrandModelsQueryValidator()
        {
            // RuleFor(x => x.BrandModelCode)
            //     .NotNull().WithErrorCode("PQ-E-PC-001")
            //     .NotEmpty().WithErrorCode("PQ-E-PC-002"); //.WithMessage("Please specify a valid BrandModel code.");
            // RuleFor(x => x.BrandModelName)
            //     .NotNull().WithErrorCode("PQ-E-PN-003")
            //     .NotEmpty().WithErrorCode("PQ-E-PN-004")
            //     .Length(3, 250).WithErrorCode("PQ-E-PN-005"); //.WithMessage("Please specify a BrandModel name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}