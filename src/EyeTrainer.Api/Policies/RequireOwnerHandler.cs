using EyeTrainer.Api.Constants;
using EyeTrainer.Api.Data;
using EyeTrainer.Api.Validation;
using Microsoft.AspNetCore.Authorization;

namespace EyeTrainer.Api.Policies
{
    public class RequireOwnerHandler : AuthorizationHandler<RequireOwnerRequirement>
    {
        private readonly EyeTrainerApiContext _repositoryContext;
        public RequireOwnerHandler(EyeTrainerApiContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            RequireOwnerRequirement requirement)
        {
            if (context.Resource is not HttpContext) return Task.CompletedTask;

            var userIdClaim = context.User.Claims.FirstOrDefault(claim => claim.Type == "UserId");
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var httpContext = context.Resource as HttpContext;

            var appointmentIdRouteValue = httpContext.GetRouteValue("appointmentId").ToString();

            if (!int.TryParse(appointmentIdRouteValue, out int appointmentId))
            {
                return Task.CompletedTask;
            }

            var appointment = _repositoryContext.Appointment.Find(appointmentId);

            if (appointment is null)
            {
                return Task.CompletedTask;
            }

            var isDoctorOwner = context.User.IsInRole(Roles.Doctor) && appointment.DoctorId == userId;
            if (isDoctorOwner)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var isUserOwner = context.User.IsInRole(Roles.Patient) && appointment.PatientId == userId;
            if (isUserOwner)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
