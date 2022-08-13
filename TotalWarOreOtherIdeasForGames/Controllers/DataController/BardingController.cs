﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class BardingController : Controller
    {
        // wonder way is it readonly and if i need to make the operation class readonly
        //private readonly TotalWarWanaBeContext _context;
        private readonly BardingsOperations operations;

        public BardingController(TotalWarWanaBeContext context)
        {
            this.operations = new BardingsOperations(context);
        }
        public async Task<IActionResult> IndexBarding(int? CurrentPage, int? PageSize)
        {
            if (CurrentPage == null || CurrentPage == 0)
            {   
                CurrentPage = 1;
            }
            if (PageSize == null || PageSize == 0)
            {
                PageSize = 5;
            }
            return View(PageModel<Barding>.ToPageModel(operations._context.Bardings,(int)CurrentPage, (int)PageSize));
        }
        public IActionResult CreateBarding()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBarding([Bind("BardingName,ArmorValue")] Barding barding)
        {
            // not looked inte so see how it is created
            operations._context.Bardings.Add(barding);
            await operations._context.SaveChangesAsync();
            return RedirectToAction("CreateBarding");

        }
        public async Task<IActionResult> DetailsBarding(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var barding = await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id);
            if (barding == null)
            {
                return NotFound();
            }
            return View(barding);
        }
        public async Task<IActionResult> EditBarding(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var barding = await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id);
            if (barding == null)
            {
                return NotFound();
            }
            return View(barding);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBarding(int id, [Bind("Id,BardingName,ArmorValue")] Barding barding)
        {
            if (id != barding.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try
                {
                    operations._context.Bardings.Update(barding);
                    await operations._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // 
                    //if ()
                    //{

                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    throw;
                }

            }
            return RedirectToAction("EditBarding");
        }

        // again Delete probabli should be a post method 
        public async Task<IActionResult> DeleteBarding(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            operations._context.Bardings.Remove(await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id));
            await operations._context.SaveChangesAsync();
            return RedirectToAction("IndexBarding");
        }
    }
}
