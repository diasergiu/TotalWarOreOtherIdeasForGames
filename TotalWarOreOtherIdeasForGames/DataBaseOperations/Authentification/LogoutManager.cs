using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations.Authentification
{
    public class LogoutManager
    {
        public bool IsLogedIt(HttpContext context)
        {
            return context.Session.GetString("UserName") == null;
        }
    }
}
