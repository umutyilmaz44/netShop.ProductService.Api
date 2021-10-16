using FluentValidation;
using netShop.Application.Features.Queries.ProductQueries;

namespace netShop.Application.Validators.ProductValidators
{
    public class GetProductDetailQueryValidator : AbstractValidator<GetProductDetailQuery>
    {
        public GetProductDetailQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithErrorCode("GPD-E-PId-001")
                .NotEmpty().WithErrorCode("GPD-E-PId-002"); //.WithMessage("Please specify a valid product id.");
        }
    }
}