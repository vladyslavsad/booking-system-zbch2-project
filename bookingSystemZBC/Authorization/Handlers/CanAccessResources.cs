using bookingSystemZBC.Authorization.Interfaces;
using bookingSystemZBC.Authorization.Requirements;
using bookingSystemZBC.Constants;
using bookingSystemZBC.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace bookingSystemZBC.Authorization.Handlers
{
    public class CanAccessResources : AuthorizationHandler<CanAccessResourcesRequirement, IOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanAccessResourcesRequirement requirement, IOwnedResource resource)
        {
            Console.WriteLine("HANDLER CALLED");
            var memberId = context.User.FindFirst(CustomClaims.MemberId)?.Value;
            var isAdmin = context.User.IsInRole(CustomRoles.Admin);

            if (isAdmin || resource.OwnerId.ToString() == memberId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
