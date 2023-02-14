using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_060223.Classes;
using MVC_060223.Models;

namespace MVC_060223.Controllers
{
    public class FilmlerController : Controller
    {
        private readonly UygulamaDbContext _db;
        public FilmlerController(UygulamaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Filmler.Include(x => x.Turler).ToList());
        }
        public IActionResult Yeni()
        {
            ViewBag.Turler = _db.Turler.ToList();
            var vm = new YeniFilmViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Yeni(YeniFilmViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Film flm = new Film()
                {
                    Ad = vm.Ad,
                    Puan = vm.Puan!.Value,
                    Yil = vm.Yil,
                    Turler = _db.Turler.Where(x => vm.Turler.Contains(x.Id)).ToList()
                };

                _db.Filmler.Add(flm);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Turler = _db.Turler.ToList();
            return View(vm);
        }
        public IActionResult Duzenle(int id)
        {
            var film = _db.Filmler.Include(x => x.Turler).FirstOrDefault(x => x.Id == id);
            Film? Film = _db.Filmler.Find(id);
            if (Film == null)
            {
                return NotFound();
            }
            var vm = new FilmDuzenleViewModel()
            {
                Id = Film.Id,
                Ad = Film.Ad,
                Puan = Film.Puan,
                Yil = Film.Yil,
                Turler = Film.Turler.Select(x => x.Id).ToArray()
            };
            ViewBag.Turler = _db.Turler.ToList();
            return View(vm);


        }
        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Duzenle(FilmDuzenleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var film = _db.Filmler.Find(vm.Id);
                if (film == null)  { return NotFound(); }
                _db.Entry(film).Collection(x => x.Turler).Load();
                film.Id = vm.Id;
                film.Ad = vm.Ad;
                film.Puan = vm.Puan!.Value;
                film.Turler.Clear();
                film.Turler = _db.Turler.Where(x => vm.Turler.Contains(x.Id)).ToList();
                
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Turler = _db.Turler.ToList();
            return View(vm);
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
