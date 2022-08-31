using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    [Authorize(Roles = "Normal,Manager,Admin")]
    public class BardingController : Controller
    {
        private readonly ILogger logger;
        // wonder way is it readonly and if i need to make the operation class readonly
        //private readonly TotalWarWanaBeContext _context;
        private readonly BardingsOperations operations;

        public BardingController(TotalWarWanaBeContext context, ILogger<BardingController> logger)
        {
            this.logger = logger;
            this.operations = new BardingsOperations(context);
        }
        public async Task<IActionResult> IndexBarding(int? CurrentPage, int? PageSize)
        { 
            logger.LogInformation("[Barding]: Oppened index at page {0}, and of page size {1}", CurrentPage, PageSize);
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
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult CreateBarding()
        {
            logger.LogInformation("[Barding]: Accesing the barding create View");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBarding([Bind("BardingName,ArmorValue")] Barding barding)
        {
            logger.LogInformation("[Barding}: you are creating a new Barding with name {0} and armor value {1}", barding.BardingName, barding.ArmorValue);
            // not looked inte so see how it is created
            operations._context.Bardings.Add(barding);
            await operations._context.SaveChangesAsync();
            return RedirectToAction("CreateBarding");

        }
       
        public async Task<IActionResult> DetailsBarding(int? id)
        {
            logger.LogInformation("[Barding}: you are looking at the detail of the barding with the id : {0}", id);
            if (id == null)
            {
                logger.LogDebug("[Barding]: id send was null value");
                return NotFound();
            }
            var barding = await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id);
            if (barding == null)
            {
                logger.LogDebug("[Barding]: Barding object was not found");
                return NotFound();
            }
            return View(barding);
        }
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> EditBarding(int? id)
        {
            logger.LogInformation("[Barding]: you are oppening the edit view for barding with id {0}", id);
            if (id == null)
            {
                logger.LogDebug("[Barding]: id send was null value");
                return NotFound();
            }
            var barding = await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id);
            if (barding == null)
            {
                logger.LogDebug("[Barding]: Barding object was not found");
                return NotFound();
            }
            return View(barding);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBarding(int id, [Bind("Id,BardingName,ArmorValue")] Barding barding)
        {
            logger.LogInformation("you are atempting to edit the barding with id {0}, id");
            if (id != barding.Id)
            {
                logger.LogDebug("[Barding]: id send was null value");
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
                    logger.LogCritical("[Barding]: DbUpdateConcurrencyException");
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
        [Authorize(Roles = "Manager,Admin")]
        // again Delete probabli should be a post method 
        public async Task<IActionResult> DeleteBarding(int? id)
        {
            logger.LogInformation("you are atempting to delete the barding with id {0}, id");
            if (id == null)
            {
                logger.LogDebug("[Barding]: id send was null value");
                return NotFound();
            }
            operations._context.Bardings.Remove(await operations._context.Bardings.FirstOrDefaultAsync(b => b.Id == id));
            await operations._context.SaveChangesAsync();
            return RedirectToAction("IndexBarding");
        }
    }
}
