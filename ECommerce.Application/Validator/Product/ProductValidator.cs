using ECommerce.Application.Dtos.ProductDtos;
using FluentValidation;

namespace ECommerce.Application.Validator.Product;

public class ProductValidator : AbstractValidator<ProductEditDto>
{
    #region Fields

    #endregion

    #region Constractor
    public ProductValidator()
    {
        ApplyValidationRules();
    }
    #endregion
    #region  Action
    private void ApplyValidationRules()
    {

        RuleFor(x => x.NameAr)
            .NotEmpty().WithMessage("Should not be Empty")
            .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.CategoryId)
             .NotEmpty().WithMessage("Should not be empty")
             .NotNull().WithMessage("Can not be Null");
    }

    #endregion
}
