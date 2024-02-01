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
    public class ProductDeptMastersController : Controller
    {
        private readonly ImsContext _context;

        public ProductDeptMastersController(ImsContext context)
        {
            _context = context;
        }

        // GET: ProductDeptMasters
        public async Task<IActionResult> Index()
        {
              return _context.ProductDeptMasters != null ? 
                          View(await _context.ProductDeptMasters.ToListAsync()) :
                          Problem("Entity set 'ImsContext.ProductDeptMasters'  is null.");
        }

        // GET: ProductDeptMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductDeptMasters == null)
            {
                return NotFound();
            }

            var productDeptMaster = await _context.ProductDeptMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productDeptMaster == null)
            {
                return NotFound();
            }

            return View(productDeptMaster);
        }

        // GET: ProductDeptMasters/Create
        public IActionResult Create()
        {
            var productDeptList = _context.ProductDeptMasters
                                    .OrderBy(p => p.Description)
                                    .Include(p => p.SubItemsMasters)
                                    .ToList();

            ViewBag.productDeptMasters = productDeptList;
            return View();
        }


        // POST: ProductDeptMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] ProductDeptMaster productDeptMaster)
        {
            if (ModelState.IsValid)
            {
                if (_context.ProductDeptMasters.Any(d => d.Description == productDeptMaster.Description))
                {
                    ModelState.AddModelError("Description", "This Category name already exist!.");
                    return View(productDeptMaster);
                }
                _context.Add(productDeptMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productDeptMaster);
        }

        // GET: ProductDeptMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductDeptMasters == null)
            {
                return NotFound();
            }

            var productDeptMaster = await _context.ProductDeptMasters.FindAsync(id);
            if (productDeptMaster == null)
            {
                return NotFound();
            }
            return View(productDeptMaster);
        }

        // POST: ProductDeptMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] ProductDeptMaster productDeptMaster)
        {
            if (id != productDeptMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productDeptMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductDeptMasterExists(productDeptMaster.Id))
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
            return View(productDeptMaster);
        }


        private bool ProductDeptMasterExists(int id)
        {
          return (_context.ProductDeptMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
