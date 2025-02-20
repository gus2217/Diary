using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiaryApp.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public AccountController(DiaryDbContext diaryDbContext)
        {
            _diaryDbContext = diaryDbContext;
        }
        
        public IActionResult Index()
        {
            return View(_diaryDbContext.UserAccounts.ToList());
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_diaryDbContext.UserAccounts.Any(u => u.Email == model.Email || u.UserName == model.UserName))
                {
                    ModelState.AddModelError("", "Email or Username already exists.");
                    return View(model);
                }

                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;
                account.UserName = model.UserName;

                try
                {
                    _diaryDbContext.UserAccounts.Add(account);
                    _diaryDbContext.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";

                }
                catch (DbUpdateException )
                {
                    ModelState.AddModelError("", "Please enter a unique Email or Password.");
                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _diaryDbContext.UserAccounts.Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail)
                && x.Password == model.Password).OrderByDescending(x => x.Id).FirstOrDefault();
                //var user = users.Where(x => x.UserName == model.UserNameOrEmail).FirstOrDefault();
                if (user != null)
                {
                    //Success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email), // Standard Name claim
                        new Claim("FirstName", user.FirstName), // Custom claim
                        new Claim(ClaimTypes.Role, "User") // User role
                    };


                    var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    


                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity));

                    return RedirectToAction("Index", "DiaryEntries");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct.");
                }
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.FindFirstValue("FirstName");
            return View();
        }
    }
}
