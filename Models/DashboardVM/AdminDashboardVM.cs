using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models.DashboardVM
{
    public class AdminDashboardVM
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalPrayers { get; set; }
        public int QazaPrayers { get; set; }
        public List<User> Users { get; set; }
        public List<UserProfile> UserDetails { get; set; }
        public dynamic ContentStats { get; set; }
    }
}