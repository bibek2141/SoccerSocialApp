using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("CreateUserMeetUpPosts")]
        public Response CreateMeetUpPost(MeetUpPosts meetUp)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MeetUpFunctions func = new MeetUpFunctions();
            response = func.CreateMeetUpPosts(meetUp, connection);
            return response;
        }

        [HttpPost]
        [Route("GetMeetUpsPostByID")]
        public Response GetMeetUpsPostByID(MeetUpPosts meetUp)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MeetUpFunctions func = new MeetUpFunctions();
            response = func.GetMeetUpsPostsByID(meetUp, connection);
            return response;
        }
    }
}
