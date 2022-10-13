using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace EyeTrainer.Api.Bootstrap
{
    public static class ProblemDetailsEngine
    {
        public static void SetupProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options => {
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<NullReferenceException>(StatusCodes.Status404NotFound);
                options.MapToStatusCode<ArgumentNullException>(StatusCodes.Status422UnprocessableEntity);
                options.MapToStatusCode<InvalidOperationException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<JsonPatchException>(StatusCodes.Status422UnprocessableEntity);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
        }
    }
}
