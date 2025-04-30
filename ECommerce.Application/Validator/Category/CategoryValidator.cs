using ECommerce.Application.Dtos.CategoryDtos;
using FluentValidation;

namespace ECommerce.Application.Validator.Category;

public class CategoryValidator : AbstractValidator<CategoryEditDto>
{
    #region Fields

    #endregion

    #region Constractor
    public CategoryValidator()
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
    }

    #endregion
}
