﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.Controllers
{
    public class LoginController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public LoginController(TotalWarWanaBeContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            if(UserName == null || Password == null)
            {
                return View("Index");
            }
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName && u.Password == Password);
            if(User == null)
            {
                return View("Index");
            }
            HttpContext.Session.SetString("UserName", _user.UserName);
            HttpContext.Session.SetInt32("UserType", (int)_user.UserType);
            return RedirectToAction("Home/Index");
        }
    }
}