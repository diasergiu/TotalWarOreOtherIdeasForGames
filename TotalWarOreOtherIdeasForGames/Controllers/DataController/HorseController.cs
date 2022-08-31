using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    [Authorize(Roles = "Normal,Manager,Admin")]
    public class HorseController : Controller
    {
        private readonly ILogger logger;
        private readonly HorsesOperations operations;

        public HorseController(TotalWarWanaBeContext context, ILogger<HorseController> logger)
        {
            this.logger = logger;
            this.operations = new HorsesOperations(context);
        }

        // GET: Horse
        // should look at how to make in the diferance between variable received in get and the ones in the model 
        // i should not need to have the same variable name in the get method as in the the modle mothod 
        // it violates variabe naming convention
        public async Task<IActionResult> IndexHorse(int? CurrentPage, int? PageSize) 
        {
            logger.LogInformation("[Horse]");
            if (CurrentPage == null || CurrentPage == 0)
            {
                CurrentPage = 1;
            }
            if (PageSize == null || PageSize == 0)
            {
                PageSize = 5;
            }
            return View(PageModel<Horse>.ToPageModel(operations._context.Horses,(int)CurrentPage, (int)PageSize));
        }

        // GET: Horse/Details/5
        public async Task<IActionResult> DetailsHorse(int? id)
        {
            logger.LogInformation("[Horse]");
            if (id == null)
            {
                logger.LogCritical("[Horse]:");
                return NotFound();
            }

            var horse = await operations._context.Horses
                .Include(h => h.IdBardingNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horse == null)
            {
                logger.LogCritical("[Horse]:");
                return NotFound();
            }

            return View(horse);
        }

        // GET: Horse/Create
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult CreateHorse()
        {
            logger.LogInformation("[Horse]");
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
            logger.LogInformation("[Horse]");
            if (ModelState.IsValid)
            {
                operations._context.Add(horse);
                await operations._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bardings"] = new SelectList(operations._context.Bardings, "Id", "Id", horse.IdBarding);
            return View(horse);
        }

        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> EditHorse(int? id)
        {
            logger.LogInformation("[Horse]");
            if (id == null)
            {
                logger.LogCritical("[Horse]:");
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
            logger.LogInformation("[Horse]");
            if (id != horse.Id)
            {
                logger.LogCritical("[Horse]:");
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
                        logger.LogCritical("[Horse]:");
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
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> DeleteHorse(int? id)
        {
            logger.LogInformation("[Horse]");
            if (id == null)
            {
                logger.LogCritical("[Horse]:");
                return NotFound();
            }
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
