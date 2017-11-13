using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Services.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if(e is KeyNotFoundException || e is EmptyCollectionException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.ContentType = "application/json";
            var response = JsonConvert.SerializeObject(new { errorMessage = e.Message });

            return context.Response.WriteAsync(response);
        }
    }
}
