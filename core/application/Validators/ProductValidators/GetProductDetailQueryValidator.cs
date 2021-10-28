using FluentValidation;
using NetShop.ProductService.Application.Features.Queries.ProductQueries;

namespace NetShop.ProductService.Application.Validators.ProductValidators
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