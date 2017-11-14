using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using PortfolioApplication.Middlewares;
using PortfolioApplication.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PortfolioApplication.Tests.Middlewares
{
    public class TestErrorHandlingMiddleware
    {
        [Fact]
        public async void HttpContextResponseMustConsistsOfValuesAccordingToKeyNotFoundException()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new KeyNotFoundException("Entity not found");
                }, 
                logger: loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            errorHandlingMiddleware.Invoke(context);
            
            var responseMessage = "";
            var expectedMessage = JsonConvert.SerializeObject(new { errorMessage = "Entity not found" });

            using (var memoryStream = context.Response.Body)
            {
                memoryStream.Position = 0;
                var streamReader = new StreamReader(memoryStream);
                responseMessage = streamReader.ReadToEnd();
            }

            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
            Assert.Equal(expectedMessage, responseMessage);
        }

        [Fact]
        public async void HttpContextResponseMustConsistsOfValuesAccordingToEmptyCollectionException()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new EmptyCollectionException("Collection is empty");
                }, 
                logger: loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            errorHandlingMiddleware.Invoke(context);

            var responseMessage = "";
            var expectedMessage = JsonConvert.SerializeObject(new { errorMessage = "Collection is empty" });

            using (var memoryStream = context.Response.Body)
            {
                memoryStream.Position = 0;
                var streamReader = new StreamReader(memoryStream);
                responseMessage = streamReader.ReadToEnd();
            }

            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal(StatusCodes.Status204NoContent, context.Response.StatusCode);
            Assert.Equal(expectedMessage, responseMessage);
        }

        [Fact]
        public async void HttpContextResponseMustConsistsOfDefaultValuesIfCatchedNotProcessableException()
        {
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new Exception("Something bad is happening!");
                }, 
                logger: loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await errorHandlingMiddleware.Invoke(context);

            var responseMessage = "";
            var expectedMessage = JsonConvert.SerializeObject(new { errorMessage = "Something bad is happening!" });

            using (var memoryStream = context.Response.Body)
            {
                memoryStream.Position = 0;
                var streamReader = new StreamReader(memoryStream);
                responseMessage = streamReader.ReadToEnd();
            }

            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            Assert.Equal(expectedMessage, responseMessage);
        }

        [Fact]
        public async void ProperInformationMustBeLoggedWhenKeyNotFoundExceptionIsRaised()
        {
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new KeyNotFoundException("Entity not found");
                },
                logger: loggerMock.Object);

            await errorHandlingMiddleware.Invoke(context);

            loggerMock.Verify(exp => exp.Log(It.Is<LogLevel>(x => x == LogLevel.Error),
                It.IsAny<EventId>(),
                It.IsAny<Object>(),
                It.Is<KeyNotFoundException>(y => y.Message == "Entity not found"),
                It.IsAny<Func<object, Exception, string>>()));
        }

        [Fact]
        public async void ProperInformationMustBeLoggedWhenEmptyCollectionExceptionIsRaised()
        {
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new EmptyCollectionException("Collection is empty");
                },
                logger: loggerMock.Object);

            await errorHandlingMiddleware.Invoke(context);

            loggerMock.Verify(exp => exp.Log(It.Is<LogLevel>(x => x == LogLevel.Information),
                It.IsAny<EventId>(),
                It.IsAny<Object>(),
                It.Is<EmptyCollectionException>(y => y.Message == "Collection is empty"),
                It.IsAny<Func<object, Exception, string>>()));
        }

        [Fact]
        public async void ProperInformationMustBeLoggedWhenNotHandledExceptionIsRaised()
        {
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var errorHandlingMiddleware = new ErrorHandlingMiddleware(
                next: (innerHttpContext) =>
                {
                    throw new Exception("Something bad is happening here!");
                },
                logger: loggerMock.Object);

            await errorHandlingMiddleware.Invoke(context);

            loggerMock.Verify(exp => exp.Log(It.Is<LogLevel>(x => x == LogLevel.Error),
                It.IsAny<EventId>(),
                It.IsAny<Object>(),
                It.Is<Exception>(y => y.Message == "Something bad is happening here!"),
                It.IsAny<Func<object, Exception, string>>()));
        }
    }
}
