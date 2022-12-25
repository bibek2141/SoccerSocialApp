using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SoccerSocialAppBackEnd.Models;

namespace API.Models
{
    public class CommentsFunctions
    {

        public Response CreateCommentOnDiscussionPosts(Comments comment, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ADD_COMMENT_DISCUSSION_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COMMENTS",comment.Comment);
                    cmd.Parameters.AddWithValue("@POSTID", comment.PostID);
                    cmd.Parameters.AddWithValue("@COMMENTSUSERID", comment.CommentsUserID);
                    cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now); ;
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

        /*Get List of Discussion*/
        public Response GetDiscussionComments(Comments comment, SqlConnection conn)
        {
            Response response = new Response();
            List<Comments> discussionPostComments = new List<Comments>();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_DISCUSSIONS_POST_COMMENTS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", comment.PostID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Comments dis = new Comments();
                            dis.Comment = Convert.ToString(dt.Rows[i]["Comments"]);
                            dis.PostID = Convert.ToInt32(dt.Rows[i]["POSTID"]);
                            dis.Name = Convert.ToString(dt.Rows[i]["Name"]);
                            dis.CommentsUserID = Convert.ToInt32(dt.Rows[i]["CommentsUserID"]);
                            dis.CreatedDate = Convert.ToString(dt.Rows[i]["CreatedDate"]);

                            discussionPostComments.Add(dis);
                            if (discussionPostComments.Count > 0)
                            {
                                response.ListComments = discussionPostComments;
                                response.StatusCode = 200;
                                response.StatusMessage = "Able to get list of comments on this post";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Unable to get the list of comments on this posts ";
                                response.ListComments = null;
                            }
                        }

                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve Discussion Posts.";
                        response.ListComments = null;
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
