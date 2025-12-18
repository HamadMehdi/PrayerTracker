using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models
{
    public class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string ContentURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}