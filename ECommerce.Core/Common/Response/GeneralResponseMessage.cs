namespace ECommerce.Core.Common.Response;

public class GeneralResponseMessage : IGeneralResponseMessage
{
    public GeneralResponse ErrorMessage(string msg, int StatusCode = 400, object returnEntity = null)
    {
        return new GeneralResponse
        {
            Success = false,
            Message = string.IsNullOrEmpty(msg) ? "" : msg,
            Data = returnEntity,
            StatusCode = StatusCode
        };
    }


    public GeneralResponse ErrorMessageValidation(string msg, object returnEntity = null, int StatusCode = 400)
    {
        return new GeneralResponse
        {
            Success = false,
            Message = msg,
            Data = returnEntity,
            StatusCode = StatusCode
        };
    }
    public GeneralResponse SuccessMessage(object returnEntity = null, string msg = "", int StatusCode = 200)
    {
        return new GeneralResponse
        {
            Success = true,
            Message = string.IsNullOrEmpty(msg) ? "" : msg,
            Data = returnEntity,
            StatusCode = StatusCode
        };
    }
    public GeneralResponsePagination SuccessMessagePagination(object returnEntity = null, int totalItems = 0, string msg = "DataFound", short StatusCode = 200)
    {
        return new GeneralResponsePagination
        {
            Success = true,
            Message = string.IsNullOrEmpty(msg) ? "" : msg,
            Data = returnEntity,
            StatusCode = StatusCode,
            TotalItems = totalItems
        };
    }
}
