using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlatformService.Domain.Exceptions;

namespace PlatformService.Infrastructure.Tools;

public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly bool _includeDetails;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _includeDetails = !_env.IsProduction();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(new EventId(error.HResult), error, error.Message);

                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails();

                switch (error)
                {
                    case DomainException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        problem.Title = error.Message;
                        problem.Detail = error.Message;
                        break;
                    case ValidationException:
                        problem.Status = (int)HttpStatusCode.BadRequest;
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        problem.Title = error.Message;
                        problem.Detail = error.Message;
                        break;
                    case KeyNotFoundException:
                        problem.Status = (int)HttpStatusCode.NotFound;
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        problem.Title = error.Message;
                        problem.Detail = error.Message;
                        break;
                    default:
                        problem.Status = (int)HttpStatusCode.InternalServerError;
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        problem.Title = error.Message;
                        problem.Detail = error.Message;
                        break;
                }
                if (!_includeDetails)
                {
                    problem.Detail = error.ToString();
                }

                var stream = context.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }