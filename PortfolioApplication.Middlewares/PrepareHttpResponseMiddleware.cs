using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PortfolioApplication.Middlewares
{
    public class PrepareHttpResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PrepareHttpResponseMiddleware> _logger;

        public PrepareHttpResponseMiddleware(RequestDelegate next, ILogger<PrepareHttpResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            PrepareHttpResponse(context);

            await _next(context);
        }

        private void PrepareHttpResponse(HttpContext context)
        {
            if(context.Response.StatusCode == StatusCodes.Status200OK)
            {
                if(context.Request.Method == HttpMethods.Post)
                {
                    context.Response.StatusCode = StatusCodes.Status201Created;
                }
            }
        }
    }
}
