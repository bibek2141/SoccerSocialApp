using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SoccerSocialAppBackEnd.Models;

namespace API.Models
{
    public class DiscussionFunctions
    {
        /*Create Posts*/
        public Response CreateDiscussionPosts(UserDiscussionPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE_DISCUSSION_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ADDEDBY", discussion.AddedBy);
                    cmd.Parameters.AddWithValue("@TITLE", discussion.Title);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", discussion.Descriptions);
                    cmd.Parameters.AddWithValue("@UPVOTE", 0);
                    cmd.Parameters.AddWithValue("@ISACTIVE", 1);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", 0);
                    cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Post Created Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Post Created Failed";
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

        public Response ApproveDiscussionPost(UserDiscussionPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("APPROVE_DISCUSSION_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", discussion.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Approved Post Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Approved Post Failed";
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

        public Response DeleteDiscussionPost(UserDiscussionPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE_DISCUSSION_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", discussion.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Deleted Post Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Deleting Post Failed";
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
        /*Get List of Discussion*/
        public Response GetDiscussionList(RegisterUser user, SqlConnection conn)
        {
            Response response = new Response();
            List<UserDiscussionPosts> userDiscussionPosts = new List<UserDiscussionPosts>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_DISCUSSIONS_POSTS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROLE", user.Role);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UserDiscussionPosts dis = new UserDiscussionPosts();
                            dis.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            dis.Title = Convert.ToString(dt.Rows[i]["Title"]);
                            dis.Descriptions = Convert.ToString(dt.Rows[i]["Descriptions"]);
                            dis.IsApproved = Convert.ToInt32(dt.Rows[i]["IsApproved"]);
                            dis.AddedBy = Convert.ToInt32(dt.Rows[i]["AddedBy"]);
                            dis.CreatedDate = Convert.ToString(dt.Rows[i]["CreatedDate"]);

                            userDiscussionPosts.Add(dis);
                            if (userDiscussionPosts.Count > 0)
                            {
                                response.ListUserDiscussionPosts = userDiscussionPosts;
                                response.StatusCode = 200;
                                response.StatusMessage = "Able to get list of discussion posts";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Unable to get the list of discussion posts";
                                response.RegistrationUser = null;
                            }
                        }

                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve Discussion Posts.";
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

        public Response GetDiscussionPostsByID(UserDiscussionPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            List<UserDiscussionPosts> userDiscussionPosts = new List<UserDiscussionPosts>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET__DISCUSSION_POSTS_BY_ID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", discussion.ID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            UserDiscussionPosts dis = new UserDiscussionPosts();
                            dis.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            dis.Title = Convert.ToString(dt.Rows[i]["Title"]);
                            dis.Descriptions = Convert.ToString(dt.Rows[i]["Descriptions"]);
                            dis.IsApproved = Convert.ToInt32(dt.Rows[i]["IsApproved"]);
                            dis.AddedBy = Convert.ToInt32(dt.Rows[i]["AddedBy"]);
                            dis.CreatedDate = Convert.ToString(dt.Rows[i]["CreatedDate"]);

                            userDiscussionPosts.Add(dis);
                            if (userDiscussionPosts.Count > 0)
                            {
                                response.ListUserDiscussionPosts = userDiscussionPosts;
                                response.StatusCode = 200;
                                response.StatusMessage = "Able to get list of discussion posts by ID";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Unable to get the list of discussion posts by ID";
                                response.RegistrationUser = null;
                            }
                        }

                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve Discussion Posts by ID.";
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

        //only returns one 
        public Response GetDiscussionPostByID(UserDiscussionPosts discussion, SqlConnection conn)
        {
            Response response = new Response();
            List<UserDiscussionPosts> userDiscussionPosts = new List<UserDiscussionPosts>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_DISCUSSION_POST_BY_ID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", discussion.ID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Able to get list of discussion posts by ID";
                        UserDiscussionPosts dis = new UserDiscussionPosts();
                        dis.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        dis.Title = Convert.ToString(dt.Rows[0]["Title"]);
                        dis.Descriptions = Convert.ToString(dt.Rows[0]["Descriptions"]);
                        dis.AddedBy = Convert.ToInt32(dt.Rows[0]["AddedBy"]);
                        dis.Name = Convert.ToString(dt.Rows[0]["Name"]);
                        dis.CreatedDate = Convert.ToString(dt.Rows[0]["CreatedDate"]);
                        userDiscussionPosts.Add(dis);
                        response.ListUserDiscussionPosts = userDiscussionPosts;
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to get the list of discussion posts by ID";
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
