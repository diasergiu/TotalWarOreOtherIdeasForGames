using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.DataBaseOperations.Authentification
{
    public class UserManager
    {
        private TotalWarWanaBeContext _context;
         
        public async void Login(HttpContext httpContext, User user, bool isPersistent = false)
        {
            User actualUser = _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();
            ClaimsIdentity identity = new ClaimsIdentity();
        }
    }
}
