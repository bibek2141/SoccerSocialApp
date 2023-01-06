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
        public Response CreateMeetUpPosts(MeetUpPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE_MEETUP_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ADDEDBY", discussion.AddedBy);
                    cmd.Parameters.AddWithValue("@TITLE", discussion.Title);
                    cmd.Parameters.AddWithValue("@LOCATION", discussion.Location);
                    cmd.Parameters.AddWithValue("@UPVOTE", 0);
                    cmd.Parameters.AddWithValue("@ISACTIVE", 1);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", 0);
                    cmd.Parameters.AddWithValue("@MEETUPSDATETIME", DateTime.Parse(discussion.MeetUpsDateTime));
                    

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

        public Response ApproveMeetUpPost(MeetUpPosts discussion, SqlConnection conn)
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
                        MeetUpPosts dis = new MeetUpPosts();
                        dis.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        dis.Location = Convert.ToString(dt.Rows[0]["Location"]);
                        dis.AddedBy = Convert.ToInt32(dt.Rows[0]["AddedBy"]);
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

        public Response GetMeetUpsPostsByID(MeetUpPosts meetUp, SqlConnection conn)
        {
            Response response = new Response();
            List<MeetUpPosts> meetUpPosts = new List<MeetUpPosts>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_MEETUP_POSTS_BY_ID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", meetUp.ID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            MeetUpPosts post = new MeetUpPosts();
                            post.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            post.Title = Convert.ToString(dt.Rows[i]["Title"]);
                            post.Location = Convert.ToString(dt.Rows[i]["Location"]);
                            post.IsApproved = Convert.ToInt32(dt.Rows[i]["IsApproved"]);
                            post.AddedBy = Convert.ToInt32(dt.Rows[i]["AddedBy"]);
                            post.CreatedDate = Convert.ToString(dt.Rows[i]["CreatedDate"]);
                            post.MeetUpsDateTime = Convert.ToString(dt.Rows[i]["MeetUpsDateTime"]);

                            meetUpPosts.Add(post);
                            if (meetUpPosts.Count > 0)
                            {
                                response.ListUserMeetUpPosts = meetUpPosts;
                                response.StatusCode = 200;
                                response.StatusMessage = "Able to get list of MeetUp posts by ID";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Unable to get the list of Meetup posts by ID";
                                response.ListUserMeetUpPosts = null;
                            }
                        }

                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve MeetUp Posts by ID.";
                        response.ListUserMeetUpPosts = null;
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
