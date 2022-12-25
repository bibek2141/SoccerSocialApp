using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerSocialAppBackEnd.Models
{
    public class UserMemePosts
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string AddedBy { get; set; }
        public int IsActive { get; set; }
        public int IsApproved { get; set; }
        public int UpVote { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}