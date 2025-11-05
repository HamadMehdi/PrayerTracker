using Newtonsoft.Json;
using PrayerTracker1.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PrayerTracker1.Controllers
{
    public class PrayerController : Controller
    {
        private ApplicationDbContext _context;

        public PrayerController()
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
            var today = DateTime.Today;
            var existingPrayers = _context.tbl_Prayer.Where(p => p.PrayerDate == today && (p.IsOffered || p.IsQaza)).Select(p => p.PrayerName).ToList();

            ViewBag.ExistingPrayer = JsonConvert.SerializeObject(existingPrayers);

            return View();
        }


        public ActionResult Save(Prayer Prayer)
        {
            var userId = Session["UserId"] as int?;
            var date = Prayer.PrayerDate.Value.ToString("dd/MM/yy");
            bool prayerExists = _context.tbl_Prayer.Where(e => DbFunctions.TruncateTime(e.PrayerDate) == Prayer.PrayerDate && e.PrayerName == Prayer.PrayerName && e.FK_UserID == userId).Any(e => e.IsOffered || e.IsQaza);

            if (prayerExists)
            {
                ModelState.AddModelError("PrayerName", $"This prayer has already been recorded for the selected Date {date} ");
                return View("New", Prayer);
            }

            if (Prayer.ID == 0)
            {


                _context.Database.ExecuteSqlCommand("INSERT INTO Prayers(PrayerDate, PrayerName, IsOffered, IsQaza, FK_UserID)" +
                 " VALUES ( '" + Prayer.PrayerDate + "', '" + Prayer.PrayerName + "' , '" + Prayer.IsOffered + "' , '" + Prayer.IsQaza + "' ,  '" + userId + "') ");
            }

            try
            {
                _context.SaveChanges();
                TempData["ShowSuccessAlert"] = true;
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

                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return RedirectToAction("New");

        }
        public ActionResult Edit(int Id)
        {

            return View("New");
        }
        public ActionResult Delete(int Id)
        {

            return RedirectToAction("Index");
        }

    }

}