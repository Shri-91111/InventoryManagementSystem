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
    public class TaxMastersController : Controller
    {
        private readonly ImsContext _context;

        public TaxMastersController(ImsContext context)
        {
            _context = context;
        }

        // GET: TaxMasters
        public async Task<IActionResult> Index()
        {
              return _context.TaxMasters != null ? 
                          View(await _context.TaxMasters.ToListAsync()) :
                          Problem("Entity set 'ImsContext.TaxMasters'  is null.");
        }

        // GET: TaxMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaxMasters == null)
            {
                return NotFound();
            }

            var taxMaster = await _context.TaxMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxMaster == null)
            {
                return NotFound();
            }

            return View(taxMaster);
        }

        // GET: TaxMasters/Create
        public IActionResult Create()
        {
            var taxMasterList = _context.TaxMasters
                                    .OrderBy(t => t.PercentageDesc)
                                    .Include(t => t.ItemWorks)
                                    .ToList();

            ViewBag.taxMasters = taxMasterList;
            return View();
        }


        // POST: TaxMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PercentageDesc")] TaxMaster taxMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxMaster);
        }

        // GET: TaxMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaxMasters == null)
            {
                return NotFound();
            }

            var taxMaster = await _context.TaxMasters.FindAsync(id);
            if (taxMaster == null)
            {
                return NotFound();
            }
            return View(taxMaster);
        }

        // POST: TaxMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PercentageDesc")] TaxMaster taxMaster)
        {
            if (id != taxMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxMasterExists(taxMaster.Id))
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
            return View(taxMaster);
        }

        // GET: TaxMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaxMasters == null)
            {
                return NotFound();
            }

            var taxMaster = await _context.TaxMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxMaster == null)
            {
                return NotFound();
            }

            return View(taxMaster);
        }

        // POST: TaxMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaxMasters == null)
            {
                return Problem("Entity set 'ImsContext.TaxMasters'  is null.");
            }
            var taxMaster = await _context.TaxMasters.FindAsync(id);
            if (taxMaster != null)
            {
                _context.TaxMasters.Remove(taxMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxMasterExists(int id)
        {
          return (_context.TaxMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
