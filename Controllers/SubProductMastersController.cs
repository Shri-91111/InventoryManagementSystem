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
    public class SubProductMastersController : Controller
    {
        private readonly ImsContext _context;

        public SubProductMastersController(ImsContext context)
        {
            _context = context;
        }

        // GET: SubProductMasters
        public async Task<IActionResult> Index()
        {
            var imsContext = (from subProduct in _context.SubProductMasters
                             join product in _context.ProductMasters on subProduct.Pid equals product.Id
                             select new
                             {
                                 SubProduct = subProduct,
                                 Product = product
                             }).ToList();
            ViewBag.Items = imsContext;
            return View();
        }


        // GET: SubProductMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubProductMasters == null)
            {
                return NotFound();
            }

            var subProductMaster = await _context.SubProductMasters
                .Include(s => s.PidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProductMaster == null)
            {
                return NotFound();
            }

            return View(subProductMaster);
        }

        // GET: SubProductMasters/Create
        public IActionResult Create()
        {
            ViewData["Pid"] = new SelectList(_context.ProductMasters, "Id", "ProductName");
            var subProductMasterList = _context.SubProductMasters
                                         .OrderBy(s => s.SubProdcutDesc)
                                         .Include(s => s.PidNavigation)
                                         .ToList();

            ViewBag.subProductMasters = subProductMasterList;

            return View();
        }

        // POST: SubProductMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubProdcutDesc,Pid")] SubProductMaster subProductMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subProductMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pid"] = new SelectList(_context.ProductMasters, "Id", "ProductName", subProductMaster.Pid);
            return View(subProductMaster);
        }

        // GET: SubProductMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubProductMasters == null)
            {
                return NotFound();
            }

            var subProductMaster = await _context.SubProductMasters.FindAsync(id);
            if (subProductMaster == null)
            {
                return NotFound();
            }
            ViewData["Pid"] = new SelectList(_context.ProductMasters, "Id", "ProductName", subProductMaster.Pid);
            return View(subProductMaster);
        }

        // POST: SubProductMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubProdcutDesc,Pid")] SubProductMaster subProductMaster)
        {
            if (id != subProductMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subProductMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubProductMasterExists(subProductMaster.Id))
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
            ViewData["Pid"] = new SelectList(_context.ProductMasters, "Id", "ProductName", subProductMaster.Pid);
            return View(subProductMaster);
        }

        // GET: SubProductMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubProductMasters == null)
            {
                return NotFound();
            }

            var subProductMaster = await _context.SubProductMasters
                .Include(s => s.PidNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subProductMaster == null)
            {
                return NotFound();
            }

            return View(subProductMaster);
        }

        // POST: SubProductMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubProductMasters == null)
            {
                return Problem("Entity set 'ImsContext.SubProductMasters'  is null.");
            }
            var subProductMaster = await _context.SubProductMasters.FindAsync(id);
            if (subProductMaster != null)
            {
                _context.SubProductMasters.Remove(subProductMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubProductMasterExists(int id)
        {
          return (_context.SubProductMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
