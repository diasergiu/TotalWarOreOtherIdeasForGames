using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TotalWarDLA.Models;
using TotalWarOreOtherIdeasForGames.DataBaseOperations;
using TotalWarOreOtherIdeasForGames.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FormationController : Controller
    {
        private readonly TotalWarWanaBeContext _context;
       // private readonly FormationsOperation formationOperations;

        public FormationController(TotalWarWanaBeContext context)
        {
            _context = context;
            //formationOperations = new FormationsOperation(context);
        }

        // GET: Formation
        public async Task<IActionResult> IndexFormation()
        {
            var totalWarWanaBeContext = _context.Formations.Include(s => s.IdHorseNavigation).Include(s => s.IdSoldierNavigation);
            return View(await totalWarWanaBeContext.ToListAsync());
        }

        // GET: Formation/Details/5
        public async Task<IActionResult> DetailsFormation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await _context.Formations
                .Include(s => s.IdHorseNavigation)
                .Include(s => s.IdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // GET: Formation/Create
        public IActionResult CreateFormation()
        {
            ViewData["Horses"] = new SelectList(_context.Horses, "Id", "BreedName");
            ViewData["Soldiers"] = new SelectList(_context.SoldierModels, "Id", "SoldierName");
            ViewData["Traits"] = new SelectList(_context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(_context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(_context.Factions, "Id", "FactionName");
            return View();
        }

        // POST: Formation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFormation( FormationViewModel formation)
        {
            if (ModelState.IsValid)
            {
                //formationOperations.SaveFormations(formation);
                _context.Formations.Add(formation.Formation_);
                
                for (int i = 0; i < formation.Traits_.Length; i++)
                {
                    _context.FormationTraits.Add(new FormationTrait(formation.Formation_, await _context.Traits.FirstOrDefaultAsync(q => q.Id == formation.Traits_[i])));
                }
                for (int i = 0; i < formation.Factions_.Length; i++)
                {
                    _context.FactionFormations.Add(new FactionFormation(await _context.Factions.FirstOrDefaultAsync(q => q.Id == formation.Factions_[i]), formation.Formation_));
                }
                for (int i = 0; i < formation.Items_.Length; i++)
                {
                    _context.ItemFormations.Add(new ItemFormation(await _context.Items.FirstOrDefaultAsync(q => q.Id == formation.Items_[i]), formation.Formation_));
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["horses"] = new SelectList(_context.Horses, "Id", "Id", formation.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(_context.SoldierModels, "Id", "Id", formation.Formation_.IdSoldier);
            ViewData["Traits"] = new SelectList(_context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(_context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(_context.Factions, "Id", "FactionName");
            return View(formation);
        }

        // GET: Formation/Edit/5
        public async Task<IActionResult> EditFormation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            FormationViewModel formationViewModel = new FormationViewModel();
            formationViewModel.Formation_ = await _context.Formations.FindAsync(id);
            var listItems = _context.ItemFormations.Where(i => i.IdFormation == id);
            int i = 0;
            formationViewModel.Items_ = new int[listItems.Count()];
            foreach(var commonItem in listItems)
            {
                formationViewModel.Items_[i] = commonItem.IdItem;
                i++;
            }
            var listTraits = _context.FormationTraits.Where(i => i.IdLeft == id);
            i = 0;
            formationViewModel.Traits_ = new int[listTraits.Count()];
            foreach (var commonTrait in listTraits)
            {
                formationViewModel.Traits_[i] = commonTrait.IdRight;
                i++;
            }
            var listFactions = _context.FactionFormations.Where(i => i.IdFormation == id);
            i = 0;
            formationViewModel.Factions_ = new int[listFactions.Count()];
            foreach (var commonFaction in listFactions)
            {
                formationViewModel.Factions_[i] = commonFaction.IdFaction;
                i++;
            }
            if (formationViewModel.Formation_ == null)
            {
                return NotFound();
            }
            ViewData["horses"] = new SelectList(_context.Horses, "Id", "Id", formationViewModel.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(_context.SoldierModels, "Id", "Id", formationViewModel.Formation_.IdSoldier);
            ViewData["Traits"] = new SelectList(_context.Traits, "Id", "TraitName");
            ViewData["Items"] = new SelectList(_context.Items, "Id", "ItemName"); ;
            ViewData["Factions"] = new SelectList(_context.Factions, "Id", "FactionName");
            return View(formationViewModel);
        }

        // POST: Formation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFormation(int id,  FormationViewModel formationViewModel)
        {
            if (id != formationViewModel.Formation_.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formationViewModel.Formation_); 
                    // Slap stick this function with ctrl + v
                    var oldListItemFormation = _context.ItemFormations.Where(ff => ff.IdFormation == formationViewModel.Formation_.Id).ToList();
                    foreach (int newId in formationViewModel.Items_)
                    {
                        bool isNew = true;

                        foreach (ItemFormation FF in oldListItemFormation)
                        {
                            if (FF.IdItem == newId)
                            {
                                isNew = false;
                                break;
                            }
                        }
                        if (isNew)
                        {
                            _context.ItemFormations.Add(new ItemFormation(_context.Items.FirstOrDefault(itm => itm.Id == newId), formationViewModel.Formation_));
                        }
                    }

                    foreach (ItemFormation ff in oldListItemFormation)
                    {
                        bool needRemove = true;
                        foreach (int newId in formationViewModel.Items_)
                        {
                            if (ff.IdItem == newId)
                            {
                                needRemove = false;
                                break;
                            }
                        }
                        if (needRemove)
                        {
                            _context.ItemFormations.Remove(ff);
                        }
                    }

                    var oldListTraitFormation = _context.FormationTraits.Where(ff => ff.IdLeft == formationViewModel.Formation_.Id).ToList();
                    foreach (int newId in formationViewModel.Traits_)
                    {
                        bool isNew = true;

                        foreach (FormationTrait FF in oldListTraitFormation)
                        {
                            if (FF.IdRight == newId)
                            {
                                isNew = false;
                                break;
                            }
                        }
                        if (isNew)
                        {
                            _context.FormationTraits.Add(new FormationTrait(formationViewModel.Formation_, _context.Traits.FirstOrDefault(itm => itm.Id == newId)));
                        }
                    }

                    foreach (FormationTrait ff in oldListTraitFormation)
                    {
                        bool needRemove = true;
                        foreach (int newId in formationViewModel.Traits_)
                        {
                            if (ff.IdRight == newId)
                            {
                                needRemove = false;
                                break;
                            }
                        }
                        if (needRemove)
                        {
                            _context.FormationTraits.Remove(ff);
                        }
                    }

                    var oldListFactionFormation = _context.FactionFormations.Where(ff => ff.IdFormation == formationViewModel.Formation_.Id).ToList();
                    foreach (int newId in formationViewModel.Factions_)
                    {
                        bool isNew = true;

                        foreach (FactionFormation FF in oldListFactionFormation)
                        {
                            if (FF.IdFaction == newId)
                            {
                                isNew = false;
                                break;
                            }
                        }
                        if (isNew)
                        {
                            _context.FactionFormations.Add(new FactionFormation(_context.Factions.FirstOrDefault(itm => itm.Id == newId), formationViewModel.Formation_));
                        }
                    }

                    foreach (FactionFormation ff in oldListFactionFormation)
                    {
                        bool needRemove = true;
                        foreach (int newId in formationViewModel.Factions_)
                        {
                            if (ff.IdFaction == newId)
                            {
                                needRemove = false;
                                break;
                            }
                        }
                        if (needRemove)
                        {
                            _context.FactionFormations.Remove(ff);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormationExists(formationViewModel.Formation_.Id))
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
            ViewData["Horses"] = new SelectList(_context.Horses, "Id", "Id", formationViewModel.Formation_.IdHorse);
            ViewData["Soldiers"] = new SelectList(_context.SoldierModels, "Id", "Id", formationViewModel.Formation_.IdSoldier);
            return View(formationViewModel);
        }

        // GET: Formation/Delete/5
        [ActionName("DeleteFormation")]
        public async Task<IActionResult> DeleteFormation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Formation = await _context.Formations
                .Include(s => s.IdHorseNavigation)
                .Include(s => s.IdSoldierNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Formation == null)
            {
                return NotFound();
            }

            return View(Formation);
        }

        // POST: Formation/Delete/5
        [HttpPost, ActionName("DeleteFormation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Formation = await _context.Formations.FindAsync(id);
            _context.Formations.Remove(Formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormationExists(int id)
        {
            return _context.Formations.Any(e => e.Id == id);
        }
    }
}
