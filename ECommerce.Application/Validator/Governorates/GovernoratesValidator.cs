using ECommerce.Application.Dtos.GovernoratesDtos;
using FluentValidation;


namespace ECommerce.Application.Validator.Governorates;

public class GovernoratesValidator : AbstractValidator<GovernoratesDto>
{
    #region Fields

    #endregion

    #region Constractor
    public GovernoratesValidator()
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
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("Should not be Empty")
            .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.DeliverdFees)
            .NotEmpty().WithMessage("Should not be empty")
            .NotNull().WithMessage("Can not be Null");
    }

    #endregion

}
