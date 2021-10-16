using FluentValidation;
using netShop.Domain.Entities;

namespace netShop.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.productCode).NotNull().NotEmpty().WithMessage("Please specify a valid product code.");
            RuleFor(x => x.productName).NotNull().NotEmpty().Length(3, 250).WithMessage("Please specify a product name.");
            
            // Complex Properties
            // RuleFor(x => x.Address).InjectValidator();

            // Other way
            //RuleFor(x => x.Address).SetValidator(new AddressValidator());

            // Collections of Complex Types
            //RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}