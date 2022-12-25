using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SoccerSocialAppBackEnd.Models;

namespace API.Models
{
    public class MemeFunctions
    {
        /*Create Meme Posts*/
        public Response CreateMemePosts(UserMemePosts meme, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE_MEME_POST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ADDEDBY", meme.AddedBy);
                    cmd.Parameters.AddWithValue("@TITLE", meme.Title);
                    cmd.Parameters.AddWithValue("@IMAGE", meme.Image);
                    cmd.Parameters.AddWithValue("@UPVOTE", 0);
                    cmd.Parameters.AddWithValue("@ISACTIVE", meme.IsActive);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", meme.IsApproved);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Meme Post Created Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Meme Post Created Failed";
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

        public Response ApproveMemePost(UserMemePosts meme, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("APPROVE_MEME_POSTS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", meme.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Approved Meme Post Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Approved Meme Post Failed";
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

        /*Get List of Meme Post*/
        public Response GetMemeList(SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("GET_MEME_POST_LIST", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Able to get list of memes";
                        UserMemePosts dis = new UserMemePosts();
                        dis.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        dis.Image = Convert.ToString(dt.Rows[0]["Image"]);
                        dis.AddedBy = Convert.ToString(dt.Rows[0]["AddedBy"]);
                        response.ListUserMemePosts.Add(dis);
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to retrieve Memes Post Lists.";
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
