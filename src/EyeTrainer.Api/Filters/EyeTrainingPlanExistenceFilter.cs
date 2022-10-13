using EyeTrainer.Api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EyeTrainer.Api.Filters
{
    public class EyeTrainingPlanExistenceFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var appointmentId = GetInt(context, "appointmentId");
            var eyeTrainingPlanId = GetInt(context, "eyeTrainingPlanId");

            if (appointmentId == null) return;
            if (eyeTrainingPlanId == null) return;

            var dbContext = context.HttpContext.RequestServices.GetService<EyeTrainerApiContext>();

            var appointment = dbContext.Appointment
                .Include(app => app.EyeTrainingPlans)
                .FirstOrDefault(app => app.Id == appointmentId);

            if (appointment == null)
            {
                context.Result = (context.Controller as ControllerBase).NotFound();
                return;
            }

            var doesExist = appointment.EyeTrainingPlans.Any(plan => plan.Id == eyeTrainingPlanId);
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
