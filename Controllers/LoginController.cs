using IMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly ImsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LoginController(ImsContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
              return _context.LoginMasters != null ? 
                          View(await _context.LoginMasters.ToListAsync()) :
                          Problem("Entity set 'ImsContext.LoginMasters'  is null.");
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoginMasters == null)
            {
                return NotFound();
            }

            var loginMaster = await _context.LoginMasters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginMaster == null)
            {
                return NotFound();
            }

            return View(loginMaster);
        }

        // GET: Login/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoginMaster model)
        {
            if (ModelState.IsValid)
            {
                // Validate credentials (You should replace this with your actual validation logic)
                var isValidCredentials = ValidateCredentials(model.UserName, model.Password);

                if (isValidCredentials)
                {
                    // Retrieve user roles from your database or wherever you store them
                    var userRoles = GetUserRoles(model.UserName);

                    // Create claims for the authenticated user
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                };

                    // Add role claims
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var userPrincipal = new ClaimsPrincipal(userIdentity);

                    // Sign in the user
                  

                    // Redirect to the desired page based on roles
                    if (userRoles.Contains("RequireAdminRole"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If authentication fails, redisplay the form with errors
            return View(model);
        }
        public IEnumerable<string> GetUserRoles(string username)
        {
            // Example: Retrieve roles from a database or any other data source
            // You should implement the actual logic based on your authentication mechanism
            // For simplicity, returning some hardcoded roles here
            if (username == "admin")
            {
                return new List<string> { "RequireAdminRole" };
            }
            else
            {
                return new List<string> { "RegularUserRole" };
            }
        }
        public bool ValidateCredentials(string username, string password)
        {
            // Validate credentials against the database
            return _context.LoginMasters.Any(user => user.UserName == username && user.Password == password);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoginMasters == null)
            {
                return NotFound();
            }

            var loginMaster = await _context.LoginMasters.FindAsync(id);
            if (loginMaster == null)
            {
                return NotFound();
            }
            return View(loginMaster);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password")] LoginMaster loginMaster)
        {
            if (id != loginMaster.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginMasterExists(loginMaster.Id))
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
            return View(loginMaster);
        }
        private bool LoginMasterExists(int id)
        {
          return (_context.LoginMasters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Create", "Login"); // Redirect to the desired page after logout
        }
    }
}
