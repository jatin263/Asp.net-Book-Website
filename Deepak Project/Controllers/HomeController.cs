using Deepak_Project.Data;
using Deepak_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deepak_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<BookModel> b = _db.Books.ToList();
            return View(b);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertBook(BookModel b) {
            b.NoOfDownloads = 0;
            _db.Books.Add(b);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string q)
        {
            IEnumerable<BookModel> b = _db.Books.Where(x=>x.Title.ToLower().Contains(q.ToLower())).ToList();
            return View(b);
        }

        public IActionResult Buy(int id)
        {
            BuyModel d = new BuyModel();
            var h = _db.Books.Find(id);
            d.Book = h;
            var g = _db.Books.FromSqlRaw("Select * from books where category={0} and id != {1}",h.Category,id).ToList();
            d.Books = g;
            return View(d);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}