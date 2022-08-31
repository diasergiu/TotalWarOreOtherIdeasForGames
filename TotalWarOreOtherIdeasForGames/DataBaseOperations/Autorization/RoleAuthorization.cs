using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models.Enum;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations.Autorization
{
    public class RoleAuthorization : IAuthorizationRequirement
    {
        public string UserAcces { get; set; }
        public RoleAuthorization(string UserAcces)
        {
            this.UserAcces = UserAcces;
        }
    }
}
