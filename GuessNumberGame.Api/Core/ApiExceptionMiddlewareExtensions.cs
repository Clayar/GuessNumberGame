using Microsoft.AspNetCore.Builder;

namespace GuessNumberGame.Api.Core
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void UseApiException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiExceptionMiddleware>();
        }
    }
}