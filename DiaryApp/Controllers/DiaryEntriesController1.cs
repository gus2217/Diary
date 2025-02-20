using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Security.Claims;

namespace DiaryApp.Controllers
{
    [Authorize]
    public class DiaryEntriesController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiaryEntriesController(DiaryDbContext diaryDbContext, UserManager<ApplicationUser> userManager)
        {
            _diaryDbContext = diaryDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge(); // Ensure user is logged in

            var entries =   _diaryDbContext.DiaryEntries.ToList();
            return View(entries);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiaryEntry obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            obj.UserId = int.Parse(userId);

            var currentUser =await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                obj.UserAccount = currentUser.UserAccount;
            }


            if (obj != null && obj.Title.Length < 3)
                ModelState.AddModelError("Title", "Title too short");

            if (ModelState.IsValid)
            {
                 _diaryDbContext.DiaryEntries.Add(obj);
                _diaryDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            DiaryEntry? diary = _diaryDbContext.DiaryEntries.Find(id);

            if (diary == null)
                return NotFound();
            return View(diary);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntry obj)
        {
            if (obj != null && obj.Title.Length < 3)
                ModelState.AddModelError("Title", "Title too short");

            if (ModelState.IsValid)
            {
                _diaryDbContext.DiaryEntries.Update(obj);
                _diaryDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            DiaryEntry? diary = _diaryDbContext.DiaryEntries.Find(id);

            if (diary == null)
                return NotFound();
            return View(diary);
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntry obj)
        {
            
                _diaryDbContext.DiaryEntries.Remove(obj);
                _diaryDbContext.SaveChanges();
                return RedirectToAction("Index");
            
        }
    }
}
