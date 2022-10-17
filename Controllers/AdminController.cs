using demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace demo.Controllers
{
    public class AdminController : Controller
    {
        private readonly SampleContext db;
        private readonly ISession session;
        public AdminController(SampleContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminTbl obj)
        {
            var result = (from i in db.AdminTbls
                          where i.Email == obj.Email && i.Password == obj.Password
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("name", result.AdminName);
                return RedirectToAction("Index", "Movie");
            }
            else
                return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}

