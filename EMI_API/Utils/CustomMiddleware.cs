namespace EMI_API.Utils
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomMiddleware> logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // se ejecuta antes del endpoint 
            logger.LogInformation("Info request: {Method} {Path}", context.Request.Method, context.Request.Path);

            //Permite que se ejecute las invocaciones del  endpoint solicitado
            await next(context);

            // se ejecuta despues del endpoint 
            logger.LogInformation("Finaliza el CustomMiddleware");
        }
    }
}
