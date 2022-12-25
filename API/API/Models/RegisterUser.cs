using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerSocialAppBackEnd.Models
{
    public class RegisterUser
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        
        public int Role { get; set; }

        public int IsApproved { get; set; }
    }
}