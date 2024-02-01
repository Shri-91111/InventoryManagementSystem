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
    public class SubItemsMastersController : Controller
    {
        private readonly ImsContext _context;

        public SubItemsMastersController(ImsContext context)
        {
            _context = context;
        }

        // GET: SubItemsMasters
        public async Task<IActionResult> Index()
        {
            var imsContext = _context.SubItemsMasters.Include(s => s.Mainitem);
            return View(await imsContext.ToListAsync());
        }

        // GET: SubItemsMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubItemsMasters == null)
            {
                return NotFound();
            }

            var subItemsMaster = await _context.SubItemsMasters
                .Include(s => s.Mainitem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subItemsMaster == null)
            {
                return NotFound();
            }

            return View(subItemsMaster);
        }

        // GET: SubItemsMasters/Create
        public IActionResult Create()
        {
            ViewData["MainitemId"] = new SelectList(_context.ProductDeptMasters, "Id", "Description");
            return View();
        }

        // POST: SubItemsMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubItemName,MainitemId")] SubItemsMaster subItemsMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subItemsMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainitemId"] = new SelectList(_context.ProductDeptMasters, "Id", "Id", subItemsMaster.MainitemId);
            return View(subItemsMaster);
        }

        // GET: SubItemsMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubItemsMasters == null)
            {
                return NotFound();
            }

            var subItemsMaster = await _context.SubItemsMasters.FindAsync(id);
            if (subItemsMaster == null)
            {
                return NotFound();
            }
            ViewData["MainitemId"] = new SelectList(_context.ProductDeptMasters, "Id", "Id", subItemsMaster.MainitemId);
            return View(subItemsMaster);
        }

        // POST: SubItemsMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubItemName,MainitemId")] SubItemsMaster subItemsMaster)
        {
            if (id != subItemsMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subItemsMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubItemsMasterExists(subItemsMaster.Id))
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
            ViewData["MainitemId"] = new SelectList(_context.ProductDeptMasters, "Id", "Id", subItemsMaster.MainitemId);
            return View(subItemsMaster);
        }

        // GET: SubItemsMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubItemsMasters == null)
            {
                return NotFound();
            }

            var subItemsMaster = await _context.SubItemsMasters
                .Include(s => s.Mainitem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subItemsMaster == null)
            {
                return NotFound();
            }

            return View(subItemsMaster);
        }

        // POST: SubItemsMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubItemsMasters == null)
            {
                return Problem("Entity set 'ImsContext.SubItemsMasters'  is null.");
            }
            var subItemsMaster = await _context.SubItemsMasters.FindAsync(id);
            if (subItemsMaster != null)
            {
                _context.SubItemsMasters.Remove(subItemsMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubItemsMasterExists(int id)
        {
          return (_context.SubItemsMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
