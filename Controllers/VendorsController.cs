using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace IMS.Controllers
{
    
    public class VendorsController : Controller
    {
        private readonly ImsContext _context;

        public VendorsController(ImsContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
              return _context.Vendors != null ? 
                          View(await _context.Vendors.ToListAsync()) :
                          Problem("Entity set 'ImsContext.Vendors'  is null.");
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

      
        public IActionResult Create()
        {
            var vendorList = _context.Vendors.OrderBy(v => v.VendorName).ToList();
            ViewBag.vendors = vendorList;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorId,VendorName,VendorAddress,PhoneNumber,EmailAddress")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate mobile number
                if (_context.Vendors.Any(v => v.PhoneNumber == vendor.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Mobile number already exists.");
                    return View(vendor);
                }

                // Check for duplicate email address
                if (_context.Vendors.Any(v => v.EmailAddress == vendor.EmailAddress))
                {
                    ModelState.AddModelError("EmailAddress", "Email address already exists.");
                    return View(vendor);
                }
                if (_context.Vendors.Any(v => v.VendorName == vendor.VendorName))
                {
                    ModelState.AddModelError("VendorName", "Vendor name already exists.");
                    return View(vendor);
                }

                // If no duplicates, proceed to add and save
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vendor);
        }


       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vendors == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorId,VendorName,VendorAddress,PhoneNumber,EmailAddress")] Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.VendorId))
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
            return View(vendor);
        }

       

        private bool VendorExists(int id)
        {
          return (_context.Vendors?.Any(e => e.VendorId == id)).GetValueOrDefault();
        }
    }
}
