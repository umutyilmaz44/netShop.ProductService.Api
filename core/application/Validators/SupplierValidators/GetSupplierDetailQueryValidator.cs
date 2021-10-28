using FluentValidation;
using NetShop.ProductService.Application.Features.Queries.SupplierQueries;

namespace NetShop.ProductService.Application.Validators.SupplierValidators
{
    public class GetSupplierDetailQueryValidator : AbstractValidator<GetSupplierDetailQuery>
    {
        public GetSupplierDetailQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("GSD-E-SId-001")
                .NotEmpty().WithErrorCode("GSD-E-SId-002"); //.WithMessage("Please specify a valid Supplier id.");
        }
    }
}