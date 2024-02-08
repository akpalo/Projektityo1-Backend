using Microsoft.Extensions.Primitives;
using System.Drawing.Text;


namespace VarausJarjestelma.Middleware
{
    public class ApikeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";

        //konstruktori
        public ApikeyMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        //käsittely
        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key missing");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apikey=appSettings.GetValue<string>(APIKEYNAME);
            if(!apikey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            await _next(context);
        }
            
        

    }
}
