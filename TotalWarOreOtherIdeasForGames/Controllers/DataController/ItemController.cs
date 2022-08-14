using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TotalWarDLA.Models;
using TotalWarDLA.Models.Pagination;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.DataBaseOperations.ImageConverter;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class ItemController : Controller
    {
        private readonly ILogger logger;
        private readonly ItemsOperations operations;

        public ItemController(TotalWarWanaBeContext context, ILogger<ItemController> logger)
        {
            this.logger = logger;
            this.operations = new ItemsOperations(context);
        }

        public async Task<IActionResult> IndexItem(int? CurrentPage, int? PageSize)
        {
            logger.LogInformation("[Item]:");
            if (CurrentPage == null || CurrentPage == 0)
            {
                CurrentPage = 1;
            }
            if (PageSize == null || PageSize == 0)
            {
                PageSize = 5;
            }
            return View(PageModel<Item>.ToPageModel(operations._context.Items,(int) CurrentPage, (int)PageSize));
        }
        public async Task<IActionResult> DetailsItem(int? id)
        {
            logger.LogInformation("[Item]");
            if (id == null)
            {
                logger.LogCritical("[Item]:");
                return NotFound();
            }

            var item = await operations._context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.imgsrc = ImageConverter.Decrypt(item.Image);
            return View(item);
        }
        public  IActionResult CreateItem()
        {
            logger.LogInformation("[Item]");
            ViewData["Formations"] = new SelectList(operations._context.Formations, "Id", "FormationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(/*[Bind("Id,StaminaCost,SpeedCost,ItemName")]*/[FromForm]ItemViewModel item)
        {
            logger.LogInformation("[Item]");
            if (ModelState.IsValid)
            {
                item.Item_.Image = ImageConverter.Encryption(item.ImageFile);
                operations._context.Items.Add(item.Item_);
                foreach(int id in item.Formations_)
                {
                    operations._context.ItemFormations.Add(new ItemFormation(item.Item_,await operations._context.Formations.FirstOrDefaultAsync(f => f.Id == id)));
                }
                await operations._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> EditItem(int? id)
        {
            logger.LogInformation("[Item]");
            if (id == null)
            {
                logger.LogCritical("[Item]:");
                return NotFound();
            }
            ViewData["Formations"] = new SelectList(operations._context.Formations, "Id", "FormationName");
            ItemViewModel itemViewModel = new ItemViewModel(await operations._context.Items.FindAsync(id));
            if (itemViewModel.Item_ == null)
            {
                return NotFound();
            }
            // pe bune trebuie sa schimb asta 
            List<ItemFormation> itemsFormation = operations._context.ItemFormations.Where(if_ => if_.IdItem == id).ToList();
            itemViewModel.Formations_ = new int[itemsFormation.Count];
            int i = 0;
            foreach(var item in itemsFormation)
            {
                itemViewModel.Formations_[i] = item.IdFormation;
                i++;
            }
            return View(itemViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(int id,ItemViewModel item)
        {
            logger.LogInformation("[Item]");
            if (id != item.Item_.Id)
            {
                logger.LogCritical("[Item]:");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    item.Item_.Image = ImageConverter.Encryption(item.ImageFile);
                    operations._context.Update(item.Item_);
                    var listItemFormation = operations._context.ItemFormations.Where(if_ => if_.IdItem == item.Item_.Id);
                    
                    foreach (var itemFormation in listItemFormation){
                        bool needsRemove = true;
                        for(int i  = 0; i < item.Formations_.Length; i++){
                            if(itemFormation.IdFormation == item.Formations_[i]){
                                needsRemove = false;
                                break;
                            }
                        }
                        if (needsRemove){
                            operations._context.ItemFormations.Remove(itemFormation);
                        }
                    }

                    for(int i = 0; i < item.Formations_.Length; i++){
                        bool needsAdded = true;
                        foreach(var itemFormation in listItemFormation){
                            if(itemFormation.IdFormation == item.Formations_[i]) {
                                needsAdded = false;
                            }                            
                        }
                        if (needsAdded)
                        {
                            operations._context.ItemFormations.Add(new ItemFormation(item.Item_, operations._context.Formations.FirstOrDefault(f => f.Id == item.Formations_[i])));
                        }
                    }
                    await operations._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Item_.Id))
                    {
                        logger.LogCritical("[Item]:");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        public async Task<IActionResult> DeleteItem(int? id)
        {
            logger.LogInformation("[Item]");
            if (id == null)
            {
                logger.LogCritical("[Item]:");
                return NotFound();
            }
            var item = await operations._context.Items.FindAsync(id);
            operations._context.Items.Remove(item);
            await operations._context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return operations._context.Items.Any(e => e.Id == id);
        }
    }
}
