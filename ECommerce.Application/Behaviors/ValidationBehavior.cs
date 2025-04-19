using ECommerce.Application.Dtos.CategoryDtos;
using ECommerce.Application.Dtos.GovernoratesDtos;
using ECommerce.Application.Dtos.OrdersDtos;
using ECommerce.Application.Dtos.ProductDtos;
using ECommerce.Application.Validator.Category;
using ECommerce.Application.Validator.Governorates;
using ECommerce.Application.Validator.Order;
using ECommerce.Application.Validator.Product;
using FluentValidation.Results;

namespace ECommerce.Application.Behaviors;

public class ValidationUtility
{
    public static string ValidateProduct(ProductEditDto dto)
    {
        var message = string.Empty;
        ProductValidator validator = new ProductValidator();
        ValidationResult results = validator.Validate(dto);
        if (!results.IsValid) message = ShowError(results);
        return message;
    }
    public static string ValidateCategory(CategoryEditDto dto)
    {
        var message = string.Empty;
        CategoryValidator validator = new CategoryValidator();
        ValidationResult results = validator.Validate(dto);
        if (!results.IsValid) message = ShowError(results);
        return message;
    }
    public static string ValidateGovernorate(GovernoratesDto dto)
    {
        var message = string.Empty;
        GovernoratesValidator validator = new GovernoratesValidator();
        ValidationResult results = validator.Validate(dto);
        if (!results.IsValid) message = ShowError(results);
        return message;
    }
    public static string ValidateOrder(OrdersEditDto dto)
    {
        var message = string.Empty;
        OrderValidator validator = new OrderValidator();
        ValidationResult results = validator.Validate(dto);
        if (!results.IsValid) message = ShowError(results);
        return message;
    }

    private static string ShowError(ValidationResult results)
    {
        var message = string.Empty;
        foreach (var error in results.Errors)
        {
            message += $"({error.PropertyName + ": " + error.ErrorMessage})  & ";
        }
        return message;
    }
}