using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IMS.Controllers
{
    public class ItemWorksController : Controller
    {
        private readonly ImsContext _context;

        public ItemWorksController(ImsContext context)
        {
            _context = context;
        }

        // GET: ItemWorks
        public async Task<IActionResult> Index()
        {
            var result = await _context.ItemWorks
                    .Join(_context.ThroghMasters,
                      item => item.ThroghId,
                      related => related.Id,
                      (item, related) => 
                      new 
                      { Item = item, 
                        FirstRelated = related 
                      })
                    .Join(_context.Vendors,
                      temp => temp.Item.VendoId,
                      related => related.VendorId,
                      (temp, related) => new 
                      {   temp.Item, 
                          temp.FirstRelated, 
                          SecondRelated = related 
                      })
                    .Join(_context.ProductDeptMasters,
                      temp => temp.Item.TypeId,
                      related => related.Id,
                      (temp, related) => new 
                      {   temp.Item, 
                          temp.FirstRelated,
                          temp.SecondRelated, 
                          ThirdRelated = related
                      })
                    .Join(_context.TaxMasters,
                   temp=>temp.Item.Tax,
                   related=>related.Id,
                   (temp,related)=>new 
                   {
                       temp.Item,
                       temp.FirstRelated,
                       temp.SecondRelated,
                       temp.ThirdRelated,
                       Fourthtable=related
                   })
                  .Join(_context.ProductMasters,
                  temp=>temp.Item.Pid,
                  related=>related.Id,
                  (temp, related) => new
                  {
                      temp.Item,
                      temp.FirstRelated,
                      temp.SecondRelated,
                      temp.ThirdRelated,
                      temp.Fourthtable,
                     Fifthrelated=related,
                  })
                  .Join(_context.SubProductMasters,
                  temp=>temp.Item.ItemDesc,
                  related=>related.Id,
                  (temp, related) => new 
                  {
                  temp.Item,
                  temp.FirstRelated,
                  temp.SecondRelated,
                  temp.ThirdRelated,
                      temp.Fourthtable,
                      temp.Fifthrelated,
                      Sixthrelated = related,

                  })

                .ToListAsync();
                ViewBag.result=result;
                return View();
        }


        // GET: ItemWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemWorks == null)
            {
                return NotFound();
            }

            var itemWork = await _context.ItemWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemWork == null)
            {
                return NotFound();
            }

            return View(itemWork);
        }

        // GET: ItemWorks/Create
        public IActionResult Create()
        {
            var vendormst = _context.Vendors.ToList();
            ViewBag.vendormaster = vendormst;
            var typemaster = _context.ProductDeptMasters.ToList();
            ViewBag.type = typemaster;
            var throghmaster = _context.ThroghMasters.ToList();
            ViewBag.throghmst = throghmaster;
            ViewData["subtypeid"] = new SelectList(_context.SubItemsMasters,"Id", "SubItemName").ToList();
            ViewData["Taxid"]=new SelectList(_context.TaxMasters, "Id", "PercentageDesc").ToList();
            ViewData["Pid"]= new SelectList(_context.ProductMasters, "Id", "ProductName").ToList();
            ViewData["SubItemid"]= new SelectList(_context.SubProductMasters, "Id", "SubProdcutDesc").ToList();


            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subtypeid,Pid,Id,ItemDesc,TypeId,VendoId,NumOfQuantity,PurchaseDate,ThroghId,Amount,Tax")] ItemWork itemWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var vendormst = _context.Vendors.ToList();
            ViewBag.vendormaster = vendormst;
            var typemaster = _context.ProductDeptMasters.ToList();
            ViewBag.type = typemaster;
            var throghmaster = _context.ThroghMasters.ToList();
            ViewBag.throghmst = throghmaster;
            return View(itemWork);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubItems(int mainItemId)
        {
            // Implement logic to retrieve subitems based on the selected main item
            var subItems = await _context.SubProductMasters
                                           .Where(i => i.Pid == mainItemId)
                                           .ToListAsync();

            return Json(new SelectList(subItems, "Id", "SubProdcutDesc"));
        }

        // GET: ItemWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemWorks == null)
            {
                return NotFound();
            }

            var itemWork = await _context.ItemWorks.FindAsync(id);
            if (itemWork == null)
            {
                return NotFound();
            }
            var vendormst = _context.Vendors.ToList();
            ViewBag.vendormaster = vendormst;
            var typemaster = _context.ProductDeptMasters.ToList();
            ViewBag.type = typemaster;
            var throghmaster = _context.ThroghMasters.ToList();
            ViewBag.throghmst = throghmaster;
            return View(itemWork);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subtypeid,Pid,Id,ItemDesc,TypeId,VendoId,NumOfQuantity,PurchaseDate,ThroghId,Amount,Tax")] ItemWork itemWork)
        {
            if (id != itemWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemWorkExists(itemWork.Id))
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
            var vendormst = _context.Vendors.ToList();
            ViewBag.vendormaster = vendormst;
            var typemaster = _context.ProductDeptMasters.ToList();
            ViewBag.type = typemaster;
            var throghmaster = _context.ThroghMasters.ToList();
            ViewBag.throghmst = throghmaster;
            return View(itemWork);
        }

       

        private bool ItemWorkExists(int id)
        {
          return (_context.ItemWorks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
