using API.Models;
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
    public class CommentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CommentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("DiscussionPostsComment")]
        public Response RegisterUser(Comments comment)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            CommentsFunctions func = new CommentsFunctions();
            response = func.CreateCommentOnDiscussionPosts(comment, connection);
            return response;
        }

        [HttpPost]
        [Route("CommentsListByID")]
        public Response GetCommentsListByID(Comments comment)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            CommentsFunctions func = new CommentsFunctions();
            response = func.GetDiscussionComments(comment, connection);
            return response;
        }
    }
}
