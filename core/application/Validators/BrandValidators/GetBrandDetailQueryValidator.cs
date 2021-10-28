using FluentValidation;
using NetShop.ProductService.Application.Features.Queries.BrandQueries;

namespace NetShop.ProductService.Application.Validators.BrandValidators
{
    public class GetBrandDetailQueryValidator : AbstractValidator<GetBrandDetailQuery>
    {
        public GetBrandDetailQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("GBD-E-BId-001")
                .NotEmpty().WithErrorCode("GBD-E-BId-002"); //.WithMessage("Please specify a valid Brand id.");
        }
    }
}