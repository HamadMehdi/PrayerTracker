using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PrayerTracker1.Models;
using PrayerTracker1.Models.ReportsVM;

namespace PrayerTracker1.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext _context;
        string query = "";

        public ReportsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {

            return View();
        }
        public ActionResult ResetPrayer()
        {
            var userId = Session["UserID"] as int?;
            var TodayDate = DateTime.Today;
            _context.Database.ExecuteSqlCommand("DELETE FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + TodayDate + "') AND FK_UserID = '" + userId + "'");

            return RedirectToAction("PrayerReport");
        }
        public ActionResult PrayerReport(ReportsVM vm)
        {
            var userId = Session["UserID"] as int?;

            if (vm.Date == default(DateTime))
            {
                vm.Date = DateTime.Today;
            }

            var reportVm = new ReportsVM
            {
                ReportType = vm.ReportType ?? "daily",
                SelectedPrayer = vm.SelectedPrayer ?? "All Prayers",
                Date = vm.Date,
                DateRange = vm.DateRange,
                Todate = vm.EndDate,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };
            ViewBag.ExistingPrayer = JsonConvert.SerializeObject(vm.DateRange == "custom");
            try
            {
                string query;
                DateTime startDate, endDate;
                string dateRangeDisplay;

                switch (reportVm.ReportType.ToLower())
                {
                    case "daily":
                        startDate = endDate = reportVm.Date.Date;
                        query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + reportVm.Date + "') AND FK_UserID = '" + userId + "'";
                        dateRangeDisplay = reportVm.Date.ToString("MMMM dd, yyyy");
                        break;

                    case "weekly":
                        startDate = reportVm.Date.AddDays(-(int)reportVm.Date.DayOfWeek);
                        endDate = startDate.AddDays(6);
                        query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' ORDER BY PrayerDate ASC";
                        dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                        break;

                    case "monthly":
                        startDate = new DateTime(reportVm.Date.Year, reportVm.Date.Month, 1);
                        endDate = startDate.AddMonths(1).AddDays(-1);
                        query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' ORDER BY PrayerDate ASC";
                        dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                        break;

                    case "category":
                        var prayerName = reportVm.SelectedPrayer?.ToLower();
                        bool isAllPrayers = prayerName == "all prayers" || prayerName == "all";

                        if (!isAllPrayers && !new[] { "fajr", "dhuhr", "asr", "maghrib", "isha" }.Contains(prayerName))
                        {
                            ModelState.AddModelError("SelectedPrayer", "Invalid prayer type selected.");
                            return View(reportVm);
                        }

                        switch (reportVm.DateRange?.ToLower())
                        {
                            case "today":
                                startDate = endDate = DateTime.Today;
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + DateTime.Today + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + DateTime.Today + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = DateTime.Today.ToString("MMMM dd, yyyy");
                                break;

                            case "yesterday":
                                startDate = endDate = DateTime.Today.AddDays(-1);
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + DateTime.Today.AddDays(-1) + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) = CONVERT(date, '" + DateTime.Today.AddDays(-1) + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = DateTime.Today.AddDays(-1).ToString("MMMM dd, yyyy");
                                break;

                            case "thisweek":
                                var diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
                                startDate = DateTime.Today.AddDays(-diff);
                                endDate = startDate.AddDays(6);
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                                break;

                            case "lastweek":
                                diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
                                var startOfThisWeek = DateTime.Today.AddDays(-diff);
                                startDate = startOfThisWeek.AddDays(-7);
                                endDate = startOfThisWeek.AddDays(-1);
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                                break;

                            case "thismonth":
                                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                                endDate = DateTime.Today;
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                                break;

                            case "lastmonth":
                                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                                endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) >= CONVERT(date,'" + startDate + "') AND CONVERT(date, PrayerDate) <= CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                                break;

                            case "custom":
                               
                                startDate = reportVm.StartDate;
                                endDate = reportVm.EndDate;
                                if (isAllPrayers)
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) BETWEEN CONVERT(date, '" + startDate + "') AND CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "'";
                                else
                                    query = "SELECT FK_UserID, PrayerDate, PrayerName, IsOffered, IsQaza FROM Prayers WHERE CONVERT(date, PrayerDate) BETWEEN CONVERT(date, '" + startDate + "') AND CONVERT(date, '" + endDate + "') AND FK_UserID = '" + userId + "' AND PrayerName = '" + prayerName + "'";
                                dateRangeDisplay = $"{startDate:MMMM dd, yyyy} - {endDate:MMMM dd, yyyy}";
                                break;

                            default:
                                ModelState.AddModelError("DateRange", "Invalid date range selected.");
                                return View(reportVm);
                        }
                        break;

                    default:
                        ModelState.AddModelError("ReportType", "Invalid report type selected.");
                        return View(reportVm);
                }

                var records = _context.Database.SqlQuery<ReportsVM.PrayerRecord>(query).ToList();
                int completed = 0, qaza = 0;

                foreach (var record in records)
                {
                    if (record.IsQaza)
                    {
                        record.Status = "Qaza";
                        qaza++;
                    }
                    else if (record.IsOffered)
                    {
                        record.Status = "Completed";
                        completed++;
                    }
                }

                reportVm.Prayers = records;
                reportVm.TotalPrayers = records.Count;
                reportVm.Completed = completed;
                reportVm.Qaza = qaza;
                reportVm.FormattedDateRange = dateRangeDisplay;

                ViewBag.ReportSummary = new
                {
                    reportType = reportVm.ReportType,
                    SelectedPrayer = reportVm.SelectedPrayer ?? "All Prayers",
                    dateRange = reportVm.FormattedDateRange,
                    totalPrayers = reportVm.TotalPrayers.ToString(),
                    completed = reportVm.Completed.ToString(),
                    qaza = reportVm.Qaza.ToString()
                };

                return View(reportVm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while generating the report.");
                return View(reportVm);
            }
        }
    }
}
