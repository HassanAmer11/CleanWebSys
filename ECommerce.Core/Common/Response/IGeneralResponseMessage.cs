namespace ECommerce.Core.Common.Response
{
    public interface IGeneralResponseMessage
    {
        GeneralResponse SuccessMessage(object returnEntity = null, string msg = "", int StatusCode = 200);
        GeneralResponse ErrorMessage(string msg, int StatusCode = 400, object returnEntity = null);
        GeneralResponse ErrorMessageValidation(string msg, object returnEntity = null, int StatusCode = 400);
        GeneralResponsePagination SuccessMessagePagination(object returnEntity = null, int TotalItems = 0, string msg = "DataFound", Int16 StatusCode = 200);
    }
}
