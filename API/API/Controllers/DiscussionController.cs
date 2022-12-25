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
    public class DiscussionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DiscussionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("CreateDiscussionPosts")]
        public Response CreateDiscussionPost(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.CreateDiscussionPosts(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("ListDiscussionPosts")]
        public Response GetDiscussionsList(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.GetDiscussionList(user,connection);
            return response;
        }

        [HttpPost]
        [Route("GetDiscussionPostsByID")]
        public Response GetDiscussionsPostsByID(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.GetDiscussionPostsByID(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("GetDiscussionPostByID")]
        public Response GetDiscussionPostByID(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.GetDiscussionPostByID(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("ApproveDiscussionPosts")]
        public Response ApproveUser(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.ApproveDiscussionPost(discussion, connection);
            return response;
        }

        [HttpPost]
        [Route("DeleteDiscussionPost")]
        public Response DeleteDiscussionPost(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            DiscussionFunctions func = new DiscussionFunctions();
            response = func.DeleteDiscussionPost(discussion, connection);
            return response;
        }

    }
}
