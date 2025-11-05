using System;
using System.Collections.Generic;

namespace PrayerTracker1.Models.ReportsVM
{
    public class ReportsVM
    {
       
        public string ReportType { get; set; } = "daily";
        public string SelectedPrayer { get; set; } = "All Prayers";
        public DateTime Date { get; set; } = DateTime.Today;
        public string DateRange { get; set; }
        public DateTime Todate { get; set; }
        public DateTime StartDate { get; set; }= DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;


        public List<PrayerRecord> Prayers { get; set; } = new List<PrayerRecord>();

     
        public string FormattedDateRange { get; set; }
        public int TotalPrayers { get; set; }
        public int Completed { get; set; }
        public int Qaza { get; set; }
        public int Missed { get; set; }
        public string CompletionRate =>
            TotalPrayers > 0 ? $"{Math.Round((double)Completed / TotalPrayers * 100)}%" : "0%";

     
        public class PrayerRecord
        {
            public DateTime PrayerDate { get; set; }
            public string PrayerName { get; set; }
            public string Status { get; set; }
            public bool IsOffered { get; set; }
            public bool IsQaza { get; set; }
            public bool IsMissed { get; set; }
        }
    }
}