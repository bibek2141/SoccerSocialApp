using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SoccerSocialAppBackEnd.Models;

namespace API.Models
{
    public class MeetUpFunctions
    {
        /*Create Posts*/
        public Response CreateMeetUpPosts(UserMeetUpPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE_MEETUP_POST", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ADDEDBY", discussion.AddedBy);
                    cmd.Parameters.AddWithValue("@TITLE", discussion.Title);
                    cmd.Parameters.AddWithValue("@DESCRIPTIONS", discussion.Location);
                    cmd.Parameters.AddWithValue("@UPVOTE", 0);
                    cmd.Parameters.AddWithValue("@ISACTIVE", discussion.IsActive);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", discussion.IsApproved);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "MeetUp Post Created Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "MeetUp Post Created Failed";
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public Response ApproveMeetUpPost(UserMeetUpPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("APPROVE_MEETUP_POST", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", discussion.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Approved MeetUp Post Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Approved MeetUp Post Failed";
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        /*Get List of MeetUp Post*/
        public Response GetMeetUpList(SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_MEETUP_POST_LIST", conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Able to get list of MeetUps";
                        UserMeetUpPosts dis = new UserMeetUpPosts();
                        dis.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        dis.Location = Convert.ToString(dt.Rows[0]["Location"]);
                        dis.AddedBy = Convert.ToString(dt.Rows[0]["AddedBy"]);
                        response.ListUserMeetUpPosts.Add(dis);
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve MeetUp Post Lists.";
                        response.RegistrationUser = null;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}
