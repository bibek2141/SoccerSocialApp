using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerSocialAppBackEnd.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<RegisterUser> ListUser { get; set; }
        public RegisterUser RegistrationUser { get; set; }
        public List<UserMemePosts> ListUserMemePosts { get; set; }
        public List<UserDiscussionPosts> ListUserDiscussionPosts { get; set; }
        public List<MeetUpPosts> ListUserMeetUpPosts { get; set; }

        public List<Comments> ListComments { get; set; }

    }
}