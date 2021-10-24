using FluentValidation;
using netShop.Application.Features.Queries.SupplierQueries;

namespace netShop.Application.Validators.SupplierValidators
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