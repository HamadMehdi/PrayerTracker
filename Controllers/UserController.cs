using Microsoft.AspNet.Identity;
using PrayerTracker1.Models;
using PrayerTracker1.Models.UserVM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrayerTracker1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
      
       
        public ActionResult Save(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
              
                return View("Signup", userVM);
            }

            try
            {
                var existingUser = _context.tbl_User.FirstOrDefault(u => u.UserName == userVM.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(userVM);
                }
                var user = new User
                {
                    UserName = userVM.UserName,
                    Password = userVM.Password,
                  
                };

                _context.tbl_User.Add(user);
                _context.SaveChanges();

                 var userId = user.ID;

                var userProfile = new UserProfile
                {
                    Fk_UserID = userId, 
                    FirstName = userVM.UserProfile.FirstName,
                    LastName = userVM.UserProfile.LastName,
                    Email = userVM.UserProfile.Email,
                    Address = userVM.UserProfile.Address,
                    ContactNo = userVM.UserProfile.ContactNo,
                    Role = "User", 
                    status = userVM.UserProfile.status, 
                    CreatedAt = DateTime.Now 
                };

                _context.tbl_UserProfile.Add(userProfile);
                _context.SaveChanges();

                TempData["ShowSuccessAlert"] = true;
                TempData["MessageSuccess"] = "Registration successful! Please log in.";

                return RedirectToAction("Login", "Home");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        ModelState.AddModelError("", message); 
                    }
                }
                return View("Signup", userVM); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the user. Please try again.");
                return View("Signup", userVM); 
            }
        }

        public ActionResult UserDashboard()
        {
            var userId = Session["UserId"] as int?;
            var today = DateTime.Today;
            var todayStart = DateTime.Today;
            var todayEnd = todayStart.AddDays(1);
            var qazaPrayers = _context.tbl_Prayer.Count(e => e.IsQaza == true && DbFunctions.TruncateTime(e.PrayerDate) == today && e.FK_UserID == userId).ToString();
            var prayers = _context.tbl_Prayer
                .Where(p => p.FK_UserID == userId.Value &&
                       p.PrayerDate >= todayStart &&
                       p.PrayerDate < todayEnd)
                .ToList();
            var TodayPrayer = _context.tbl_Prayer
    .Where(e => e.FK_UserID == userId && DbFunctions.TruncateTime(e.PrayerDate) == today).Count();

            return View(new UserDashboardVM
            {
                UserName = _context.tbl_UserProfile
                           .Where(u => u.Fk_UserID == userId)
                           .Select(u => u.FirstName)
                           .FirstOrDefault(),
                TotalPrayer = TodayPrayer,
                QazaPrayer = qazaPrayers,
                Prayers = prayers,
               
            });
        }

        [HttpGet]
        public ActionResult TogglePrayerStatus(int prayerId, string prayerName, string PrayerType)
        {
            var userId = Session["UserId"] as int?;

            if (prayerId == 0)
            {
                var newPrayer = new Prayer
                {
                    FK_UserID = userId.Value,
                    PrayerName = prayerName,
                    PrayerDate = DateTime.Now,
                    IsOffered = PrayerType == "Completed",
                    IsQaza = PrayerType != "Completed",
                    IsMissed = false
                };
                _context.tbl_Prayer.Add(newPrayer);
            }
            else 
            {
                var Prayers = _context.tbl_Prayer.Find(prayerId);
                if (Prayers != null)
                {
                    Prayers.PrayerDate = DateTime.Now;
                    Prayers.IsOffered = PrayerType == "Completed";
                    Prayers.IsQaza = PrayerType != "Completed";
                    Prayers.IsMissed = false;
                }
            }

            _context.SaveChanges();
            return RedirectToAction("UserDashboard");
        }
        public ActionResult ResetPrayer()
        {
            var userId = Session["UserID"] as int?;
            var TodayDate = DateTime.Today;
            _context.Database.ExecuteSqlCommand("DELETE FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + TodayDate + "') AND FK_UserID = '" + userId + "'");

            return RedirectToAction("UserDashboard");
        }
    }


}