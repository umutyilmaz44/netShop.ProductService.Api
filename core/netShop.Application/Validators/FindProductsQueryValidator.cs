using FluentValidation;
using netShop.Application.Features.Queries;

namespace netShop.Application.Validators
{
    public class FindProductsQueryValidator : AbstractValidator<FindProductsQuery>
    {
        public FindProductsQueryValidator()
        {
            RuleFor(x => x.ProductCode).NotNull().NotEmpty().WithMessage("Please specify a valid product code.");
            RuleFor(x => x.ProductName).NotNull().NotEmpty().Length(3, 250).WithMessage("Please specify a product name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}