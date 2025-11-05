using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models.UserVM
{
    public class UserDashboardVM
    {
        public string UserName { get; set; }
        public int TotalPrayer { get; set; }
        public string UserInitials { get; set; }
        public string QazaPrayer { get; set; }
        public List<Prayer> Prayers { get; set; }
        public PrayerStatsViewModel PrayerStatus { get; set; }
        public CommunityRankViewModel CommunityRank { get; set; }

        public List<PrayerTimeViewModel> TodaysPrayerTimes { get; set; }
        public MonthlyConsistencyViewModel MonthlyConsistency { get; set; }

        public List<ActivityViewModel> RecentActivities { get; set; }


        public List<GoalViewModel> Goals { get; set; }
    }

   
    public class PrayerStatsViewModel
    {
        public int TodaysPrayersCompleted { get; set; }
        public int WeeklyConsistencyPercentage { get; set; }
        public int MonthlyStreakDays { get; set; }
    }

    public class CommunityRankViewModel
    {
        public string Rank { get; set; }
        public string RankDescription { get; set; }
    }

    public class PrayerTimeViewModel
    {
        public string PrayerName { get; set; }
        public string PrayerDescription { get; set; }
        public string PrayerTime { get; set; }
        public string Status { get; set; }
    }

    public class MonthlyConsistencyViewModel
    {
        public string ChartPlaceholder { get; set; }
        public string ChartDescription { get; set; }
    }

    public class ActivityViewModel
    {
        public string ActivityType { get; set; }
        public DateTime ActivityDate { get; set; }
        public string IconClass { get; set; }
        public string IconColor { get; set; }
    }

    public class GoalViewModel
    {
        public string GoalName { get; set; }
        public int ProgressPercentage { get; set; }
        public string ProgressDescription { get; set; }
        public bool IsCompleted { get; set; }
    }

}
