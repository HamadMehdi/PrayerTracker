using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models.ReportsVM
{
    public class ReportSummaryVM
    {
        public string ReportType { get; set; }
        public string SelectedPrayer { get; set; }
        public string DateRange { get; set; }
        public string TotalPrayers { get; set; }
        public string Completed { get; set; }
        public string Qaza { get; set; }
    }
}