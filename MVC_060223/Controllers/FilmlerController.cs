using Microsoft.AspNetCore.Mvc;
using MVC_060223.Classes;

namespace MVC_060223.Controllers
{
    public class FilmlerController : Controller
    {
        private readonly UygulamaDbContext _db;
        public FilmlerController(UygulamaDbContext db) 
        {
        _db=db;
        }
        public IActionResult Index()
        {
            return View(_db.Filmler.ToList());
        }
        public IActionResult Yeni()
        {
            ViewBag.Turler=_db.Turler.ToList();
            return View();
        }
        
        [HttpPost]
        public IActionResult Yeni(Film film)
        {
            if (ModelState.IsValid)
            {
                _db.Filmler.Add(film);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Turler = _db.Turler.ToList();
            return View();
        }
        public IActionResult Duzenle(int id)
        {
            Film? Film = _db.Filmler.Find(id);
            if (Film == null) { return NotFound(); }

            return View(Film);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Duzenle(Film Film)
        {
            if (ModelState.IsValid)
            {
                _db.Update(Film);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Sil(int id)
        {
            Film? Film = _db.Filmler.Find(id);
            if (Film == null)
                return NotFound();

            return View(Film);
        }
        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult SilOnay(int id)
        {
            Film? film = _db.Filmler.Find(id);
            if (film == null)
                return NotFound();

            _db.Remove(film);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
