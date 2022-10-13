using EyeTrainer.Api.Constants;
using Microsoft.AspNetCore.Authorization;

namespace EyeTrainer.Api.Policies
{
    public class AdministratorPrivilegesHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var isAdministrator = context.User.IsInRole(Roles.Administrator);
            if (!isAdministrator) return Task.CompletedTask;

            foreach (var requirement in context.PendingRequirements)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
