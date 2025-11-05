using PrayerTracker1.Models;
using PrayerTracker1.Models.UserVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrayerTracker1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {

            var user = (from u in _context.tbl_User
                        join up in _context.tbl_UserProfile on u.ID equals up.Fk_UserID
                        where u.UserName == loginVM.username && u.Password == loginVM.password
                        select new
                        {
                            FK_UserID = u.ID,
                            Role = up.Role
                        }).FirstOrDefault();
          

            if (user != null)
            {
                Session["UserId"] = user.FK_UserID;
                Session["Role"] = user.Role;

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("UserDashboard", "User");
                }
            }

            TempData["Message"] = "Invalid username or password.";
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}