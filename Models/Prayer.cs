using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models
{
    public class Prayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public decimal FK_UserID { get; set; }
        public DateTime? PrayerDate { get; set; }
        public string PrayerName { get; set; }
        public bool IsOffered { get; set; }
        public bool IsMissed { get; set; }
      
        public bool IsQaza { get; set; }
       

    }
}