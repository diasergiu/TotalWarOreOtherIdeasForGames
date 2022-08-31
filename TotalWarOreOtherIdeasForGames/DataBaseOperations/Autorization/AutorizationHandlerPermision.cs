using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations.Autorization
{
    public class AutorizationHandlerPermision : AuthorizationHandler<RoleAuthorization>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorization requirement)
        {
            // this is just a shot in the dark
            var autorizationLevel = context.User.Claims.FirstOrDefault(c => c.Type == "UserType"); 

            if(autorizationLevel.Value == requirement.UserAcces)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
