using EyeTrainer.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EyeTrainer.Api.Filters
{
    public class AppointmentExistenceFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var appointmentId = GetInt(context, "appointmentId");
            if (appointmentId == null) return;

            var dbContext = context.HttpContext.RequestServices.GetService<EyeTrainerApiContext>();
            
            var doesExist = dbContext.Appointment.Any(app => app.Id == appointmentId);
            if (doesExist) return;
            
            context.Result = (context.Controller as ControllerBase).NotFound();
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static int? GetInt(ActionExecutingContext context, string actionArgument)
        {
            var actionArgumentValue = context.ActionArguments[actionArgument].ToString();

            if (!int.TryParse(actionArgumentValue, out int argumentValueAsInt))
            {
                return null;
            }

            return argumentValueAsInt;
        }
    }
}
