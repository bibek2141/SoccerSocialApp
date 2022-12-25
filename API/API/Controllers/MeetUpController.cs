using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.Extensions.Configuration;
using SoccerSocialAppBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetUpController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MeetUpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("MeetUp")]
        public Response CreateDiscussionPost(UserMeetUpPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MeetUpFunctions func = new MeetUpFunctions();
            response = func.CreateMeetUpPosts(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("ApproveMeetUpPosts")]
        public Response ApproveUser(UserMeetUpPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MeetUpFunctions func = new MeetUpFunctions();
            response = func.ApproveMeetUpPost(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("ListMeetUpPosts")]
        public Response GetDiscussionsList(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MeetUpFunctions func = new MeetUpFunctions();
            response = func.GetMeetUpList(connection);
            return response;
        }
    }
}
