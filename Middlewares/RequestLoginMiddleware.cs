namespace VintageApp.WebApi.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // İsteğin URL'sini al
            var url = context.Request.Path;

            // İstek zamanını al
            var requestTime = DateTime.Now;

            // Müşterinin kimliğini al (varsa)
            var clientId = context.User.Identity?.IsAuthenticated == true ? context.User.Identity.Name : "Anonymous";

            // Loglama işlemi
            _logger.LogInformation("İstek Bilgisi: URL={Url}, Zaman={RequestTime}, Müşteri Kimliği={ClientId}",
                                   url, requestTime, clientId);

            // Sonraki middleware'e geç
            await _next(context);
        }
    }
}
