using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerSocialAppBackEnd.Models
{
    public class MeetUpPosts
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int AddedBy { get; set; }
        public int IsActive { get; set; }
        public int IsApproved { get; set; }
        public int UpVote { get; set; }
        public string CreatedDate { get; set; }
        public String MeetUpsDateTime { get; set; }
    }
}