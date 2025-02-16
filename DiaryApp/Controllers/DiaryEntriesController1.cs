using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public DiaryEntriesController(DiaryDbContext diaryDbContext)
        {
            _diaryDbContext = diaryDbContext;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> entries = _diaryDbContext.DiaryEntries.ToList();

            return View(entries);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiaryEntry obj)
        {
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
