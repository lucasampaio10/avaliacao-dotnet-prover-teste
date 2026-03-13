using Assinantes.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Assinantes.API.Middlewares;

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
        catch (DomainException ex)
        {
            await EscreverResposta(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await EscreverResposta(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception)
        {
            await EscreverResposta(context, HttpStatusCode.InternalServerError, "Erro interno no servidor.");
        }
    }

    private static async Task EscreverResposta(HttpContext context, HttpStatusCode status, string mensagem)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(new { erro = mensagem });
        await context.Response.WriteAsync(json);
    }
}
