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
            IEnumerable<BookModel> b = _db.Books.FromSqlRaw("Select * From Books order by noofdownloads desc").ToList();
            return View(b);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string uEmail,string uPassword)
        {
            UserModel u = _db.Users.Where(g=>g.Email==uEmail).FirstOrDefault();
            if (u != null)
            {
                if (u.Password == uPassword)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(UserModel u)
        {
            _db.Users.Add(u);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
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
            string sql = "Select * from books where category like '%"+q+"%' or title like '%"+q+"%' or author like '%"+q+ "%' order by noofdownloads desc";
            IEnumerable<BookModel> b = _db.Books.FromSqlRaw(sql).ToList();
            return View(b);
        }

        public IActionResult Buy(int id)
        {
            BuyModel d = new BuyModel();
            var h = _db.Books.Find(id);
            h.NoOfDownloads++;
            _db.Books.Update(h);
            _db.SaveChanges();
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