using demo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace demo.Controllers
{
    public class UserController : Controller
    {
        private readonly SampleContext db;
        private readonly ISession session;
        public UserController(SampleContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserTbl obj)
        {
            var result = (from i in db.UserTbls
                          where i.Email == obj.Email && i.Password == obj.Password
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("Username", result.UserName);
                HttpContext.Session.SetInt32("UserId", result.UserId);
                return RedirectToAction("DateSearch", "Movie");
            }
            else
                return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserTbl obj)
        {
            db.UserTbls.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}

