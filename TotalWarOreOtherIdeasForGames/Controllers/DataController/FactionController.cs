using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalWarDLA.Models;
using TotalWarDLA.ViewModel;

namespace TotalWarOreOtherIdeasForGames.Controllers.DataController
{
    public class FactionController : Controller
    {

        private readonly TotalWarWanaBeContext _context;

        public FactionController()
        {
            _context = new TotalWarWanaBeContext();
        }
        public async Task<IActionResult> IndexFaction()
        {
            return View(await _context.Factions.ToListAsync());
        }
        public async Task<IActionResult> CreateFaction()
        {
            // nu imi place cum arata parte asta si ar trebui sa ma uit la ce este view models mai mult simt ca nu invat nimic aici doar aplic ce stiu deja
            FactionFormationViewModel factionFormationViewModel = new FactionFormationViewModel();
            factionFormationViewModel.ListFormations = await _context.Formations.ToListAsync();
            ViewData["formations"] = new SelectList(await _context.Formations.ToListAsync(), "IdFormation", " formationName"); ;
            return View(factionFormationViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFaction(/*[Bind("FactionName,FactionDescription,ListFormation")]*/[FromForm] FactionFormationViewModel factionFormationViewModel)
        {
            _context.Factions.Add(factionFormationViewModel.faction);
            foreach(Formation formation in factionFormationViewModel.ListFormations)
            {
                _context.FactionFormations.Add(new FactionFormation(factionFormationViewModel.faction, formation));
            }
            await _context.SaveChangesAsync();
            // look if this works 
            return View("CreateFaction");
        }
        public async Task<IActionResult> DetailsFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));

        }
        public async Task<IActionResult> EditFaction(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            return View(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFaction(int id,[Bind("IdFaction,FactionName,FactionDescription")]Faction faction)
        {
            if (faction.IdFaction != id) {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Factions.Update(faction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return RedirectToAction("EditFaction");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFaction(int? id)
        {
            if(id == null)
                {
                    return NotFound();
                }
            _context.Factions.Remove(await _context.Factions.FirstOrDefaultAsync(f => f.IdFaction == id));
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexFaction");
        }
    }
}
