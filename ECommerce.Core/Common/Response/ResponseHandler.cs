using ECommerce.Core.Resources;
using Microsoft.Extensions.Localization;

namespace ECommerce.Core.Common.Response;

public class ResponseHandler
{
    private readonly IStringLocalizer<SharedResources> _localizer;

    public ResponseHandler(IStringLocalizer<SharedResources> localizer)
    {
        _localizer = localizer;

    }
    public ResponseApp<T> Deleted<T>(string Message = null)
    {
        return new ResponseApp<T>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message == null ? _localizer[LanguageKey.DeletedSuccessfully] : Message
        };
    }
    public ResponseApp<T> Success<T>(T entity, object Meta = null)
    {
        return new ResponseApp<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer[LanguageKey.CompletedSuccessfully],
            Meta = Meta
        };
    }
    public ResponseApp<T> Updated<T>(T entity, object Meta = null)
    {
        return new ResponseApp<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = _localizer[LanguageKey.UpdateProcess],
            Meta = Meta
        };
    }
    public ResponseApp<T> Unauthorized<T>()
    {
        return new ResponseApp<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = _localizer[LanguageKey.UnAuthorized]
        };
    }
    public ResponseApp<T> BadRequest<T>(string Message = null)
    {
        return new ResponseApp<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = Message == null ? _localizer[LanguageKey.BadRequest] : Message
        };
    }

    public ResponseApp<T> UnprocessableEntity<T>(string Message = null)
    {
        return new ResponseApp<T>()
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = Message == null ? _localizer[LanguageKey.UnprocessableEntity] : Message
        };
    }

    public ResponseApp<T> NotFound<T>(string message = null)
    {
        return new ResponseApp<T>()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message == null ? _localizer[LanguageKey.NotFound] : message
        };
    }

    public ResponseApp<T> Created<T>(T entity, object Meta = null)
    {
        return new ResponseApp<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = _localizer[LanguageKey.AddSuccessfully],
            Meta = Meta
        };
    }
}
