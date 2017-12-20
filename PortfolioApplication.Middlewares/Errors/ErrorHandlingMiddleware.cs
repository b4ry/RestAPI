using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioApplication.Middlewares.Errors.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PortfolioApplication.Middlewares.Errors
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

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e is KeyNotFoundException)
            {
                _logger.LogError(exception: e, message: e.Message);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if(e is EmptyCollectionException)
            {
                _logger.LogInformation(exception: e, message: e.Message);
                context.Response.StatusCode = StatusCodes.Status204NoContent;
            }
            else if(e is DbUpdateException)
            {
                SqlException sqlException = e.InnerException as SqlException;
                e = sqlException;
                
                if (sqlException.Number == (int)SqlErrorsEnum.CannotInsertNull)
                {
                    context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                }
                else if (sqlException.Number == (int)SqlErrorsEnum.CannotInsertDuplicate)
                {
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                _logger.LogError(exception: e, message: e.Message);
            }
            else
            {
                _logger.LogError(exception: e, message: e.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.ContentType = "application/json";
            var response = JsonConvert.SerializeObject(new { errorMessage = e.Message });

            await context.Response.WriteAsync(response);
        }
    }
}
