using System.Net;
using System.Text.Json;
using TechnicalAssestmentMSA.Domain.Exceptions;

namespace TechnicalAssestmentMSA.API.Middleware;

/// <summary>
/// Middleware para tratamento global de exceções
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CnpjDuplicadoException ex)
        {
            _logger.LogWarning(ex, "CNPJ duplicado: {Cnpj}", ex.Cnpj);
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
        }
        catch (CnpjInvalidoException ex)
        {
            _logger.LogWarning(ex, "CNPJ inválido: {Cnpj}", ex.CnpjFornecido);
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Exceção de domínio: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await HandleExceptionAsync(
                context, 
                new Exception("Ocorreu um erro interno no servidor."), 
                HttpStatusCode.InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context, 
        Exception exception, 
        HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            erro = exception.Message,
            statusCode = (int)statusCode
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return context.Response.WriteAsync(
            JsonSerializer.Serialize(response, jsonOptions));
    }
}