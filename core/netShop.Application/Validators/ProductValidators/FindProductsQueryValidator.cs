using FluentValidation;
using netShop.Application.Features.Queries.ProductQueries;

namespace netShop.Application.Validators.ProductValidators
{
    public class FindProductsQueryValidator : AbstractValidator<FindProductsQuery>
    {
        public FindProductsQueryValidator()
        {
            // RuleFor(x => x.ProductCode)
            //     .NotNull().WithErrorCode("PQ-E-PC-001")
            //     .NotEmpty().WithErrorCode("PQ-E-PC-002"); //.WithMessage("Please specify a valid product code.");
            // RuleFor(x => x.ProductName)
            //     .NotNull().WithErrorCode("PQ-E-PN-003")
            //     .NotEmpty().WithErrorCode("PQ-E-PN-004")
            //     .Length(3, 250).WithErrorCode("PQ-E-PN-005"); //.WithMessage("Please specify a product name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}