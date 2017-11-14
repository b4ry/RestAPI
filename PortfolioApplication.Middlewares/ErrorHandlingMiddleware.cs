using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioApplication.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e is KeyNotFoundException)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if(e is EmptyCollectionException)
            {
                _logger.LogInformation(e.Message);
                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            else
            {
                _logger.LogError("Something bad is happening here!");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.ContentType = "application/json";
            var response = JsonConvert.SerializeObject(new { errorMessage = e.Message });

            return context.Response.WriteAsync(response);
        }
    }
}
