using Hotel.ATR1.RealPortal.Controllers;
using System.Diagnostics;

namespace Hotel.ATR1.RealPortal.AppMiddlewares
{
    public class UseTimeElapsed
    {
        private RequestDelegate _next;
        private readonly ILogger<UseTimeElapsed> _logger;

        public UseTimeElapsed(RequestDelegate next, ILogger<UseTimeElapsed> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var timer = Stopwatch.StartNew();

            await _next(context);

            timer.Stop();

            var elapsedMs = timer.Elapsed.TotalMilliseconds;
            string page = context.Request.Path;

            _logger.LogInformation($"Запрос для страницы {page} отработал за {elapsedMs} милесекунд");
        }
    }
    public static class UseTimeElapsedExtensions
    {
        public static IApplicationBuilder UseTimeElapsed(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UseTimeElapsed>();
        }
    }
}
