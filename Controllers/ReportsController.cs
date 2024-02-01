using IMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ImsContext imsContext;
        public ReportsController(ImsContext context)
        {
            imsContext = context;
        }
        public IActionResult ReportsBasedOnCategory()
        {

            return View();
        }

        public IActionResult IssuedReports()
        {
            var model = new IssuedReportsViewModel();
            ViewData["Category"] = new SelectList(imsContext.ProductDeptMasters, "Id", "Description").ToList();
            ViewData["Product"]=new SelectList(imsContext.ProductMasters,"Id", "ProductName").ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssuedReports(IssuedReportsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var issuedList = (from a in imsContext.IssuedMasters
                                  join b in imsContext.ItemWorks on a.ProductName equals b.Pid
                                  join c in imsContext.ProductMasters on b.Pid equals c.Id
                                  join d in imsContext.SubProductMasters on b.ItemDesc equals d.Id
                                  join e in imsContext.TaxMasters on b.Tax equals e.Id
                                  join f in imsContext.Employeemsts on a.IssuedPerson equals f.Fempcode
                                  join g in imsContext.Designations on a.IssuedPersonDesigntion equals g.Fdesigcode
                                  where b.TypeId == model.CategoryId && a.ProductName == model.ProductId
                                  select new IssuedMasterList
                                  {
                                      CategoryId = b.TypeId,
                                      Name = f.Fempname,
                                      Designation = g.Fdesigname,
                                      IssuedDateTime = a.IssuedDate,
                                      Description = $"{c.ProductName} - {d.SubProdcutDesc}",
                                      Price = Convert.ToDecimal(b.Amount),
                                      Quantity = Convert.ToDecimal(a.Rol),
                                  }).ToList();


                model.IssuedMasterList = issuedList;



            }
            else
            {
                ViewData["Category"] = new SelectList(imsContext.ProductDeptMasters, "Id", "Description").ToList();
                ViewData["Product"] = new SelectList(imsContext.ProductMasters, "Id", "ProductName").ToList();
                return View(model);
            }
            ViewData["Category"] = new SelectList(imsContext.ProductDeptMasters, "Id", "Description").ToList();
            ViewData["Product"] = new SelectList(imsContext.ProductMasters, "Id", "ProductName").ToList();
            return View(model);
        }


        public IActionResult EmployeesIssuedReports() 
        {
            var employeeDesignationList = from a in imsContext.Employeemsts
                                          join b in imsContext.Designations on a.Fempdesig equals b.Fdesigcode
                                          select new SelectListItem
                                          {
                                              Value = a.Fempcode.ToString(), // Make sure to handle nulls appropriately
                                              Text = $"{a.Fempname} - {b.Fdesigname}"
                                          };

            ViewData["Employees"] = employeeDesignationList.ToList();
            var model = new EmployeeReportsViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesIssuedReports(EmployeeReportsViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var issuedList = (from a in imsContext.IssuedMasters
                              join b in imsContext.ItemWorks on a.ProductName equals b.Pid
                              join c in imsContext.ProductMasters on b.Pid equals c.Id
                              join d in imsContext.SubProductMasters on b.ItemDesc equals d.Id
                              join e in imsContext.TaxMasters on b.Tax equals e.Id
                              join f in imsContext.Employeemsts on a.IssuedPerson equals f.Fempcode
                              join g in imsContext.Designations on a.IssuedPersonDesigntion equals g.Fdesigcode
                              where a.IssuedPerson==model.employeecode
                              select new IssuedMasterList
                              {
                                  CategoryId = b.TypeId,
                                  Name = f.Fempname,
                                  Designation = g.Fdesigname,
                                  IssuedDateTime = a.IssuedDate,
                                  Description = $"{c.ProductName} - {d.SubProdcutDesc}",
                                  Price = Convert.ToDecimal(b.Amount),
                                  Quantity = Convert.ToDecimal(a.Rol),
                              }).ToList();


            model.IssuedMasterList = issuedList;
            var employeeDesignationList = from a in imsContext.Employeemsts
                                          join b in imsContext.Designations on a.Fempdesig equals b.Fdesigcode
                                          select new SelectListItem
                                          {
                                              Value = a.Fempcode.ToString(), // Make sure to handle nulls appropriately
                                              Text = $"{a.Fempname} - {b.Fdesigname}"
                                          };

            ViewData["Employees"] = employeeDesignationList.ToList();
            return View(model);
        }
    }
}
