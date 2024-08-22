
using System.Net;
using System.Text.Json;
using Serilog;

public class UnifiedLoggingAndExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public UnifiedLoggingAndExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Request.EnableBuffering();

        var requestBodyStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        string requestBodyText = await new StreamReader(requestBodyStream).ReadToEndAsync();
        requestBodyStream.Seek(0, SeekOrigin.Begin);
        context.Request.Body = requestBodyStream;

        Log.Information($"Incoming request: {context.Request.Method} {context.Request.Path} {requestBodyText}");

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unhandled exception occurred.");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new { message = "An error occurred while processing your request." };
            var responseJson = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(responseJson);
        }
    }
}