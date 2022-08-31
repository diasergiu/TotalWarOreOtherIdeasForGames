using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public LoginController(TotalWarWanaBeContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            //ViewBag.Username = HttpContext.Session.GetString("UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            
            if (UserName == null || Password == null)
            {
                return View("Index");
            }
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == Password);
            if (_user == null)
            {
                return View("Index");
            }

            //// the old way
            //HttpContext.Session.SetString("UserName", _user.UserName);
            //HttpContext.Session.SetInt32("UserType", (int)_user.UserType);


            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, _user.Email));
            claims.Add(new Claim(ClaimTypes.Role, _user.UserType.ToString()));
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User _user)
        {
            if (_user == null)
            {
                return BadRequest();
            }
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //// the old way 
            //HttpContext.Session.SetString("UserName", null);
            //HttpContext.Session.SetInt32("UserType", 0);
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
