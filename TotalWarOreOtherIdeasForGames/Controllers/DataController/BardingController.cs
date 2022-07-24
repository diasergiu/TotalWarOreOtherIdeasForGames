﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class BardingController : Controller
    {

        private readonly TotalWarWanaBeContext _context;

        public BardingController()
        {
            _context = new TotalWarWanaBeContext();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bardings.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BardingName,ArmorValue")] Barding barding)
        {
            // not looked inte so see how it is created
            _context.Bardings.Add(barding);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var barding = await _context.Bardings.FirstOrDefaultAsync(b => b.IdBarding == id);
            if (barding == null)
            {
                return NotFound();
            }
            return View(barding);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var barding = await _context.Bardings.FirstOrDefaultAsync(b => b.IdBarding == id);
            if (barding == null)
            {
                return NotFound();
            }
            return View(barding);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBarding,BardingName,ArmorValue")] Barding barding)
        {
            if (id != barding.IdBarding)
            {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try
                {
                    _context.Bardings.Update(barding);
                    await _context.SaveChangesAsync();
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
            return RedirectToAction("Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            _context.Bardings.Remove(await _context.Bardings.FirstOrDefaultAsync(b => b.IdBarding == id));
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}