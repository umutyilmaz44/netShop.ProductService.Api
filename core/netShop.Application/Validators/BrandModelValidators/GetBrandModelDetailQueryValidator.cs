using FluentValidation;
using netShop.Application.Features.Queries.BrandModelQueries;

namespace netShop.Application.Validators.BrandModelValidators
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