using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrayerTracker1.Models.UserVM
{
    public class UserVM
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
       
        public string UserName { get; set; }

        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public List<UserProfile> UserProfileList { get; set; }
    }
}