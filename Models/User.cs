using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
       

    }
}