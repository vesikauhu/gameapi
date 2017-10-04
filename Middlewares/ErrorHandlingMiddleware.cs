using System.Threading.Tasks;
using gameapi.Exceptions;
using Microsoft.AspNetCore.Http;
namespace gameapi.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;

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
            catch(NotFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Specified player(s) not found!");
            }
            catch(LevelTooLowException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Player level is too low!");
            }
            catch(ItemNotFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Cannot find item with specified ID!");
            }
        }
    }
}