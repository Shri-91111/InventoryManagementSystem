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
    public class IssuedMastersController : Controller
    {
        private readonly ImsContext _context;

        public IssuedMastersController(ImsContext context)
        {
            _context = context;
        }

        // GET: IssuedMasters
        public async Task<IActionResult> Index()
        {
            var result = from t1 in _context.IssuedMasters
                         join t2 in _context.ItemWorks on t1.ProductName equals t2.Id
                         join t3 in _context.ProductMasters on t2.Pid equals t3.Id
                         join t4 in _context.Designations on t1.IssuedPersonDesigntion equals t4.Fdesigcode
                         join t5 in _context.Employeemsts on t1.IssuedPerson equals t5.Fempcode
                         join t6 in _context.SubProductMasters on t2.ItemDesc equals t6.Id

                         select new IssuedMasterViewModel
                         {
                             ProductName = t3.ProductName+"-"+t6.SubProdcutDesc,
                             IssuedDate = t1.IssuedDate,
                             Rol = t1.Rol,
                             Id=t1.Id,
                             issuedperson=t5.Fempname,
                             issuedpersondes=t4.Fdesigname,
                             remainingquantity=t2.NumOfQuantity.ToString()
                             
                         };

            ViewBag.IssuedMasterData = await result.ToListAsync();

            return View();
        }


        // GET: IssuedMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IssuedMasters == null)
            {
                return NotFound();
            }

            var issuedMaster = await _context.IssuedMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuedMaster == null)
            {
                return NotFound();
            }

            return View(issuedMaster);
        }

        // GET: IssuedMasters/Create
        public IActionResult Create()
        {

            var itemmaster = (from a in _context.ItemWorks
                              join b in _context.ProductMasters on a.Pid equals b.Id
                              join c in _context.SubProductMasters on a.ItemDesc equals c.Id
                              select new SelectListItem
                              {
                                  Value = a.Id.ToString(), // Set the value property based on your requirements
                                  Text = $"{b.ProductName}-{c.SubProdcutDesc}" // Set the text property based on your requirements
                              }).ToList();

            ViewBag.IssuedMasters = itemmaster;

            var dept =_context.ProductDeptMasters.ToList();
            ViewBag.DeptMasters = dept;
            var desig = _context.Designations.ToList();
            ViewBag.Desig = desig;
            var employees = _context.Employeemsts.ToList();
            ViewBag.Employees = employees;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,DeptType,IssuedDate,Rol,IssuedPerson,IssuedPersonDesigntion,BookNumber,PageNumber,SerialNumber")] IssuedMaster issuedMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issuedMaster);
                await _context.SaveChangesAsync();

                var inventoryItem = _context.ItemWorks.FirstOrDefault(item => item.Id == issuedMaster.ProductName);
                if (inventoryItem != null)
                {
                    inventoryItem.NumOfQuantity -= Convert.ToInt32(issuedMaster.Rol);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
             var itemmaster=_context.ItemWorks.ToList();
            ViewBag.IssuedMasters = itemmaster;
            var dept=_context.ProductDeptMasters.ToList();
            ViewBag.DeptMasters = dept;
            var desig = _context.Designations.ToList();
            ViewBag.Desig = desig;
            var employees = _context.Employeemsts.ToList();
            ViewBag.Employees = employees;
            return View(issuedMaster);
        }

        // GET: IssuedMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssuedMasters == null)
            {
                return NotFound();
            }

            var issuedMaster = await _context.IssuedMasters.FindAsync(id);
            if (issuedMaster == null)
            {
                return NotFound();
            }
            return View(issuedMaster);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,DeptType,IssuedDate,Rol,IssuedPerson,IssuedPersonDesigntion,BookNumber,PageNumber,SerialNumber")] IssuedMaster issuedMaster)
        {
            if (id != issuedMaster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuedMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuedMasterExists(issuedMaster.Id))
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
            return View(issuedMaster);
        }

        // GET: IssuedMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssuedMasters == null)
            {
                return NotFound();
            }

            var issuedMaster = await _context.IssuedMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuedMaster == null)
            {
                return NotFound();
            }

            return View(issuedMaster);
        }

        // POST: IssuedMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssuedMasters == null)
            {
                return Problem("Entity set 'ImsContext.IssuedMasters'  is null.");
            }
            var issuedMaster = await _context.IssuedMasters.FindAsync(id);
            if (issuedMaster != null)
            {
                _context.IssuedMasters.Remove(issuedMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuedMasterExists(int id)
        {
          return (_context.IssuedMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet]

        public IActionResult GetData(int value)
        {
            var result = _context.ItemWorks
                .Where(a => a.Id == value)
                .Join(
                    _context.ProductDeptMasters,
                    item => item.TypeId,  // Replace with the actual foreign key property in ItemWorks
                    other => other.Id,          // Replace with the actual primary key property in OtherTable
                    (item, other) => new
                    {
                        NoOfQuantity = item.NumOfQuantity,
                        ItemCategory = other.Description,
                        
                    })
                .FirstOrDefault();

            // Return the result as JSON
            return Json(new { result });
        }

    }
}
