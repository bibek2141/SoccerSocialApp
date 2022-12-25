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
    public class MemeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MemeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Meme")]
        public Response CreateDiscussionPost(UserMemePosts meme)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MemeFunctions func = new MemeFunctions();
            response = func.CreateMemePosts(meme, connection);
            return response;
        }

        [HttpPost]
        [Route("ApproveMemePosts")]
        public Response ApproveUser(UserMemePosts meme)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MemeFunctions func = new MemeFunctions();
            response = func.ApproveMemePost(meme, connection);
            return response;
        }

        [HttpPost]
        [Route("ListMemePosts")]
        public Response GetDiscussionsList(UserDiscussionPosts discussion)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            MemeFunctions func = new MemeFunctions();
            response = func.GetMemeList(connection);
            return response;
        }
    }
}
