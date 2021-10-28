using FluentValidation;
using NetShop.ProductService.Application.Features.Queries.BrandModelQueries;

namespace NetShop.ProductService.Application.Validators.BrandModelValidators
{
    public class GetBrandModelDetailQueryValidator : AbstractValidator<GetBrandModelDetailQuery>
    {
        public GetBrandModelDetailQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("GBMD-E-BMId-001")
                .NotEmpty().WithErrorCode("GBMD-E-BMId-002"); //.WithMessage("Please specify a valid BrandModel id.");
        }
    }
}