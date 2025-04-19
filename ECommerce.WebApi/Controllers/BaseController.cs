using ECommerce.WebApi.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController<TController>
     : ControllerBase where TController : ControllerBase
    {
        protected ILogger<TController> Logger { get; set; }
        protected IMediator Mediator { get; set; }

        protected ServiceConfiguration ServiceConfiguration { get; set; }
        protected IHttpContextAccessor HttpContextAccessor { get; set; }

        protected BaseController(IMediator Mediator,
                    ILogger<TController> Logger, IHttpContextAccessor HttpContextAccessor,
                    IOptions<ServiceConfiguration> options
        )
        {
            this.Mediator = Mediator;

            this.Logger = Logger;
            this.ServiceConfiguration = options.Value;
            this.HttpContextAccessor = HttpContextAccessor;
        }


        protected CancellationTokenSource GetCommandCancellationToken()
        {
            return new CancellationTokenSource(ServiceConfiguration.DefaultRequestTimeOutInMs);
        }
        protected CancellationTokenSource GetCommandCancellationToken(int timeOut)
        {
            return new CancellationTokenSource(timeOut);
        }
    }
}
