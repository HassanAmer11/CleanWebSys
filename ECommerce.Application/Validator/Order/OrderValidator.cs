using ECommerce.Application.Dtos.OrdersDtos;
using FluentValidation;

namespace ECommerce.Application.Validator.Order;

public class OrderValidator : AbstractValidator<OrdersEditDto>
{
    #region Fields

    #endregion

    #region Constractor
    public OrderValidator()
    {
        ApplyValidationRules();
    }
    #endregion
    #region  Action
    private void ApplyValidationRules()
    {

        RuleFor(x => x.ClientName)
            .NotEmpty().WithMessage("Should not be Empty")
            .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Should not be Empty")
            .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Should not be empty")
            .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.whatsApp)
         .NotEmpty().WithMessage("Should not be empty")
         .NotNull().WithMessage("Can not be Null");
        RuleFor(x => x.GovernorateId)
             .NotEmpty().WithMessage("Should not be empty")
             .NotNull().WithMessage("Can not be Null");
    }

    #endregion

}
