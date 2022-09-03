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
using TotalWarOreOtherIdeasForGames.Services;

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
            byte[] encrypt = EncryptDecryptService.EncryptString(Password);
            User OldUserser = _context.Users.FirstOrDefault(u => u.UserName == UserName);
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == encrypt);
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
            claims.Add(new Claim("IdUser", _user.IdUser.ToString()));
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

            _user.Password = EncryptDecryptService.EncryptString(_user.UiPassword);
            User checkIfExiest = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _user.UserName && u.Password == _user.Password);
            if (checkIfExiest != null)
            {
                ViewBag.Error = "UserName and Password already used";
                return View("Register");
            }
            _context.Users.Add(_user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //// the old way 
            //HttpContext.Session.SetString("UserName", null);
            //HttpContext.Session.SetInt32("UserType", 0);
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UserDetails(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsersList()
        {
            var Users = await _context.Users.ToListAsync();
            return View(Users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AproveUser(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AproveUser(User userAproved)
        {
            if(userAproved == null)
            {
                return BadRequest();
            }
            _context.Users.Update(userAproved);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageUsersList");
        }
    }
}
