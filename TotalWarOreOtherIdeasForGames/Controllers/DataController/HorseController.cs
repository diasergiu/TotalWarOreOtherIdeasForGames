using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class HorseController : Controller
    {
        private HorsesOperations operations;

        public HorseController(TotalWarWanaBeContext context)
        {
            this.operations = new HorsesOperations(context);
        }

        // GET: Horse
        public async Task<IActionResult> IndexHorse(PageInformationSender page) 
        {
            return View(await operations.GetPageOfHorses(page));
        }

        // GET: Horse/Details/5
        public async Task<IActionResult> DetailsHorse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await operations._context.Horses
                .Include(h => h.IdBardingNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null)
            {
                return NotFound();
            }

            return View(horse);
        }

        // GET: Horse/Create
        public IActionResult CreateHorse()
        {
            ViewData["Bardings"] = new SelectList(operations._context.Bardings, "Id", "BardingName");
            return View();
        }

        // POST: Horse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHorse([Bind("Id,AttackModifier,BreedName,DefenceModifiered,HorseStamina,HorseStrength,IdBarding")] Horse horse)
        {
            if (ModelState.IsValid)
            {
                operations._context.Add(horse);
                await operations._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bardings"] = new SelectList(operations._context.Bardings, "Id", "Id", horse.IdBarding);
            return View(horse);
        }

        // GET: Horse/Edit/5
        public async Task<IActionResult> EditHorse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await operations._context.Horses.FindAsync(id);
            if (horse == null)
            {
                return NotFound();
            }
            ViewData["Bardings"] = new SelectList(operations._context.Bardings, "Id", "Id", horse.IdBarding);
            return View(horse);
        }

        // POST: Horse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHorse(int id, [Bind("Id,AttackModifier,BreedName,DefenceModifiered,HorseStamina,HorseStrength,IdBarding")] Horse horse)
        {
            if (id != horse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    operations._context.Update(horse);
                    await operations._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseExists(horse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bardings"] = new SelectList(operations._context.Bardings, "Id", "Id", horse.IdBarding);
            return View(horse);
        }

        // GET: Horse/Delete/5
        public async Task<IActionResult> DeleteHorse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horse = await operations._context.Horses
                .Include(h => h.IdBardingNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null)
            {
                return NotFound();
            }

            return View(horse);
        }

        // POST: Horse/Delete/5
        [HttpPost, ActionName("DeleteHorse")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horse = await operations._context.Horses.FindAsync(id);
            operations._context.Horses.Remove(horse);
            await operations._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorseExists(int id)
        {
            return operations._context.Horses.Any(e => e.Id == id);
        }
    }
}
