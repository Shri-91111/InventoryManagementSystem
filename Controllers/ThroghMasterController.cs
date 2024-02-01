using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;

namespace IMS.Controllers
{
    public class ThroghMasterController : Controller
    {
        private readonly ImsContext _context;

        public ThroghMasterController(ImsContext context)
        {
            _context = context;
        }

        // GET: ThroghMaster
        public async Task<IActionResult> Index()
        {
              return _context.ThroghMasters != null ? 
                          View(await _context.ThroghMasters.ToListAsync()) :
                          Problem("Entity set 'ImsContext.ThroghMasters'  is null.");
        }

        // GET: ThroghMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ThroghMasters == null)
            {
                return NotFound();
            }

            var throghMaster = await _context.ThroghMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (throghMaster == null)
            {
                return NotFound();
            }

            return View(throghMaster);
        }

        // GET: ThroghMaster/Create
        public IActionResult Create()
        {
            var throghList = _context.ThroghMasters.OrderBy(t => t.FromDesc).ToList();
            ViewBag.throghMasters = throghList;
            return View();
        }


        // POST: ThroghMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromDesc")] ThroghMaster throghMaster)
        {
            if (ModelState.IsValid)
            {
                if (_context.ThroghMasters.Any(a => a.FromDesc == throghMaster.FromDesc))
                {
                    ModelState.AddModelError("FromDesc", "Procurement name already exist!.");
                    return View(throghMaster);  
                }

                _context.Add(throghMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(throghMaster);
        }

        // GET: ThroghMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ThroghMasters == null)
            {
                return NotFound();
            }

            var throghMaster = await _context.ThroghMasters.FindAsync(id);
            if (throghMaster == null)
            {
                return NotFound();
            }
            return View(throghMaster);
        }

        // POST: ThroghMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromDesc")] ThroghMaster throghMaster)
        {
            if (id != throghMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(throghMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThroghMasterExists(throghMaster.Id))
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
            return View(throghMaster);
        }

        // GET: ThroghMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ThroghMasters == null)
            {
                return NotFound();
            }

            var throghMaster = await _context.ThroghMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (throghMaster == null)
            {
                return NotFound();
            }

            return View(throghMaster);
        }

        // POST: ThroghMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ThroghMasters == null)
            {
                return Problem("Entity set 'ImsContext.ThroghMasters'  is null.");
            }
            var throghMaster = await _context.ThroghMasters.FindAsync(id);
            if (throghMaster != null)
            {
                _context.ThroghMasters.Remove(throghMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThroghMasterExists(int id)
        {
          return (_context.ThroghMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
