using System.Net;
using System.Text.Json;
using CutMe.Exceptions;

namespace CutMe.Configuration;

public class HttpExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public HttpExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                ArgumentException exception =>
                    (int) HttpStatusCode.BadRequest,
                ResourceNotFoundException exception =>
                    (int) HttpStatusCode.NotFound,
                ConflictException exception => 
                    (int) HttpStatusCode.Conflict,
                
                _ => (int) HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { message = error.Message });
            await response.WriteAsync(result);
        }
    }
}