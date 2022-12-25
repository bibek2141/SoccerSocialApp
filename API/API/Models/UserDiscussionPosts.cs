using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerSocialAppBackEnd.Models
{
    public class UserDiscussionPosts
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int AddedBy { get; set; }
        public int IsActive { get; set; }
        public int IsApproved { get; set; }
        public string Name { get; set; }
        public int UpVote { get; set; }
        public string CreatedDate { get; set; }
    }
}