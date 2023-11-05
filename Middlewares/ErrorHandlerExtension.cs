using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace octopus_service.Middleware
{
    public static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder UseErrorHandler(
                                          this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context
                                                    .Features
                                                    .Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        if (exceptionHandlerFeature.Error.GetType().GetProperty("StatusCode") is not null)
                            context.Response.StatusCode = (int)exceptionHandlerFeature.Error.GetType().GetProperty("StatusCode").GetValue(exceptionHandlerFeature.Error);

                        //var logger = loggerFactory.CreateLogger("ErrorHandler");
                        //logger.LogError($"Error: {exceptionHandlerFeature.Error}");

                        context.Response.ContentType = "application/json";

                        var json = new
                        {
                            context.Response.StatusCode,
                            exceptionHandlerFeature.Error.Message,
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    }
                });
            });
        }
    }

}