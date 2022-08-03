using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class ItemController : Controller
    {
        private readonly TotalWarWanaBeContext _context;

        public ItemController(TotalWarWanaBeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexItem()
        {
            return View(await _context.Items.ToListAsync());
        }
        public async Task<IActionResult> DetailsItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        public  IActionResult CreateItem()
        {
            ViewData["Formations"] = new SelectList(_context.Formations, "Id", "FormationName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(/*[Bind("Id,StaminaCost,SpeedCost,ItemName")]*/ ItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item.Item_);
                foreach(int id in item.Formations_)
                {
                    _context.ItemFormations.Add(new ItemFormation(item.Item_,await _context.Formations.FirstOrDefaultAsync(f => f.Id == id)));
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> EditItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Formations"] = new SelectList(_context.Formations, "Id", "FormationName");
            ItemViewModel itemViewModel = new ItemViewModel(await _context.Items.FindAsync(id));
            if (itemViewModel.Item_ == null)
            {
                return NotFound();
            }
            // pe bune trebuie sa schimb asta 
            List<ItemFormation> itemsFormation = _context.ItemFormations.Where(if_ => if_.IdItem == id).ToList();
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
            if (id != item.Item_.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item.Item_);
                    var listItemFormation = _context.ItemFormations.Where(if_ => if_.IdItem == item.Item_.Id);
                    
                    foreach (var itemFormation in listItemFormation){
                        bool needsRemove = true;
                        for(int i  = 0; i < item.Formations_.Length; i++){
                            if(itemFormation.IdFormation == item.Formations_[i]){
                                needsRemove = false;
                                break;
                            }
                        }
                        if (needsRemove){
                            _context.ItemFormations.Remove(itemFormation);
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
                            _context.ItemFormations.Add(new ItemFormation(item.Item_, _context.Formations.FirstOrDefault(f => f.Id == item.Formations_[i])));
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Item_.Id))
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
            return View(item);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
