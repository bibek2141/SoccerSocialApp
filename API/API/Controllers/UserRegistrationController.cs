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
    public class UserRegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
       
        public UserRegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public Response RegisterUser(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            RegistrationFunctions func = new RegistrationFunctions();
            int emailExists = func.CheckIfEmailOrPhoneExists(user, connection);
            if (emailExists == 0)
            {
                response = func.RegisterUser(user, connection);
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Email or Phone No Already Exists";
            }
           
            return response;
        }
        [HttpPost]
        [Route("Login")]
        public Response LoginUser(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            RegistrationFunctions func = new RegistrationFunctions();
            response = func.Login(user, connection);
            return response;
        }

        [HttpPost]
        [Route("Approve")]
        public Response ApproveUser(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            RegistrationFunctions func = new RegistrationFunctions();
            response = func.ApproveUser(user, connection);
            return response;
        }

        [HttpPost]
        [Route("Userlist")]
        public Response  RegisteredUser(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            RegistrationFunctions func = new RegistrationFunctions();
            response = func.GetListOfUsers(user, connection);
            return response;
        }

        [HttpPost]
        [Route("Delete")]
        public Response DeleteUser(RegisterUser user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myDb1").ToString());
            connection.Open();
            RegistrationFunctions func = new RegistrationFunctions();
            response = func.DeleteUser(user, connection);
            return response;
        }

    }
}
