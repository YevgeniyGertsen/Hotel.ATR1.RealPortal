using Hotel.ATR1.RealPortal.Controllers;

namespace Hotel.ATR1.RealPortal.AppMiddlewares
{
    public class UseLogerRequest
    {
        private RequestDelegate nextDelegate;
        private readonly ILogger<HomeController> _logger;

        public UseLogerRequest(RequestDelegate next, ILogger<HomeController> logger)
        {
            nextDelegate = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string str = string.Format("-> Method: {0}, Path: {1}",
                context.Request.Method,
                context.Request.Path);

            _logger.LogInformation("Логирование входящих запросов: {LogerRequest}", str);
            await nextDelegate.Invoke(context);
        }
    }

    public static class UseLogerRequestExtensions
    {
        public static IApplicationBuilder UseLogerRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UseLogerRequest>();
        }
    }
}
