using PrayerTracker1.Models;
using PrayerTracker1.Models.DashboardVM;
using PrayerTracker1.Models.UserVM;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrayerTracker1.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Users()
        {
            
            var Users = _context.tbl_UserProfile.ToList();
            var md = new UserVM()
            {
               
                UserProfileList = Users
            };
            return View(md);
        }
        public ActionResult Dashboard()
        {
            var totalUsers = _context.tbl_User.Count();
            var activeUsers = _context.tbl_UserProfile.Count(e => e.status == true);
            var totalPrayers = _context.tbl_Prayer.Count();
            var qazaPrayers = _context.tbl_Prayer.Count(e => e.IsQaza == true);
            var recentUsers = _context.tbl_UserProfile.OrderByDescending(e => e.CreatedAt).Take(5).ToList();
            var Users = _context.tbl_User.ToList();


            var contentStats = new ContentStatsViewModel()
            {
                PrayerGuidelines = _context.tbl_Content.Count(c => c.Category == "Prayer Guidelines"),
                QazaPrayers = _context.tbl_Content.Count(c => c.Category == "Qaza Prayers"),
                PrayerTimes = _context.tbl_Content.Count(c => c.Category == "Prayer Times")
            };
            var md = new AdminDashboardVM()
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                TotalPrayers = totalPrayers,
                QazaPrayers = qazaPrayers,
                UserDetails = recentUsers,
                ContentStats = contentStats,
                Users = Users
            };

            return View(md);
        }
        public ActionResult EditUserStatus(int id)
        {
            var user = _context.tbl_UserProfile.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.status == false)
            {
                user.status = !user.status;
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            if (user.status == true)
            {
                user.status = !user.status;
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");

        }
        public ActionResult UpdateProfile(int id)
        {
            var userDetails = (from users in _context.tbl_User
                               join profiles in _context.tbl_UserProfile on users.ID equals profiles.Fk_UserID
                               where users.ID == id
                               select new UserVM
                               {
                                   User = users,
                                   UserProfile = profiles
                               }).FirstOrDefault();

            if (userDetails == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            return View("UpdateProfile", userDetails);
        }

        public ActionResult Edit(UserVM model)

        {

            if (ModelState.IsValid)
            {
                var user = _context.tbl_User.FirstOrDefault(u => u.ID == model.User.ID);
                var profile = _context.tbl_UserProfile.FirstOrDefault(p => p.Fk_UserID == model.User.ID);

                if (user != null && profile != null)
                {

                    user.UserName = model.User.UserName;
                    user.Password = model.User.Password;
                    profile.FirstName = model.UserProfile.FirstName;
                    profile.Email = model.UserProfile.Email;
                    profile.ContactNo = model.UserProfile.ContactNo;
                    profile.Address = model.UserProfile.Address;
                    profile.status = model.UserProfile.status;


                    _context.SaveChanges();
                    TempData["Success"] = "User profile updated successfully!";
                    return RedirectToAction("UpdateProfile", new { id = model.User.ID });
                }
            }
            
            return View(model);
        }
        public ActionResult Delete(int Id)
        {
            var pro = _context.tbl_UserProfile.SingleOrDefault(c => c.Fk_UserID == Id);
            var dept = _context.tbl_User.FirstOrDefault(c => c.ID == pro.Fk_UserID);
           

            _context.tbl_User.Remove(dept);
            _context.tbl_UserProfile.Remove(pro);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        public ActionResult List()
        {
            var contents = _context.tbl_Content.OrderByDescending(c => c.CreatedAt).ToList();
            return View(contents);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Content model, HttpPostedFileBase file)
        {
            if (model.ID == 0)
            {

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Image/Videos/"), fileName);
                    file.SaveAs(path);
                    model.ContentURL = "~/Content/Image/Videos/" + fileName;
                }

                model.CreatedAt = DateTime.Now;
                _context.tbl_Content.Add(model);
            }
            else
            {
                var prayerdb = _context.tbl_Content.SingleOrDefault(c => c.ID == model.ID);
                if(prayerdb != null)
                {
                    prayerdb.Title = model.Title;
                    prayerdb.Description = model.Description;
                    prayerdb.Category = model.Category;
                    prayerdb.ContentType = model.ContentType;
                }
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/Image/Videos/"), fileName);
                    file.SaveAs(path);
                    prayerdb.ContentURL = "~/Content/Image/Videos/" + fileName;
                }

            }
                try
                {
                    _context.SaveChanges();
                    TempData["Success"] = "Content added successfully!";
                    return RedirectToAction("List");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}: {1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);

                            ModelState.AddModelError("", message);
                        }
                    }
                }
            TempData["Error"] = "Error adding content. Please check your input.";
            return View(model);
        }

        public ActionResult ContentEdit(int Id)
        {
            var contentdata = _context.tbl_Content.SingleOrDefault(c => c.ID == Id);
            if (contentdata == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            return View("Add", contentdata);
        }


        public ActionResult DeleteContent(int id)
        {
            var content = _context.tbl_Content.SingleOrDefault(c => c.ID == id);

            if (content == null)
            {
                TempData["Error"] = "Content not found.";
                return RedirectToAction("List");
            }

            if (!string.IsNullOrEmpty(content.ContentURL))
            {
                string filePath = Server.MapPath(content.ContentURL);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.tbl_Content.Remove(content);
            _context.SaveChanges();

            TempData["Success"] = "Content deleted successfully!";
            return RedirectToAction("List");
        }



    }

}