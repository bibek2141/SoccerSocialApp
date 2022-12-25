using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SoccerSocialAppBackEnd.Models;

namespace API.Models
{
    public class RegistrationFunctions
    {
        public Response RegisterUser(RegisterUser  registerUser, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand("USER_REGISTRATION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NAME", registerUser.Name);
                    cmd.Parameters.AddWithValue("@EMAIL", registerUser.Email);
                    cmd.Parameters.AddWithValue("@PASSWORD", registerUser.Password);
                    cmd.Parameters.AddWithValue("@PHONENO", registerUser.PhoneNo);
                    cmd.Parameters.AddWithValue("@ISACTIVE", 1);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", 0);
                    cmd.Parameters.AddWithValue("@ROLE", 2);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Registration Successful";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Registration Failed";
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

        public Response Login(RegisterUser registerUser, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("USER_LOGIN", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@EMAIL", registerUser.Email);
                    cmd.Parameters.AddWithValue("@PASSWORD", registerUser.Password);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "login Successful";
                        RegisterUser reg = new RegisterUser();
                        reg.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                        reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
                        reg.Email = Convert.ToString(dt.Rows[0]["Email"]);
                        reg.Role = Convert.ToInt32(dt.Rows[0]["Role"]);
                        reg.IsApproved = Convert.ToInt32(dt.Rows[0]["IsApproved"]);
                        response.RegistrationUser = reg;
                        Console.WriteLine(response.RegistrationUser.Role);
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Login Failed";
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

        public int CheckIfEmailOrPhoneExists(RegisterUser registerUser, SqlConnection conn)
        {
            int emailOrPhoneNoExists = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand("Check_Email_Or_PhoneNo_Exists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMAIL", registerUser.Email);
                    cmd.Parameters.AddWithValue("@PHONENO", registerUser.PhoneNo);

                    emailOrPhoneNoExists = (int)cmd.ExecuteScalar();
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return emailOrPhoneNoExists;
        }

        public Response ApproveUser(RegisterUser registerUser, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("APPROVE_USER_REGISTRATION", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", registerUser.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Approved Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Approved Failed";
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

        public Response DeleteUser(RegisterUser registerUser, SqlConnection conn)
        {
            Response response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE_USER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", registerUser.ID);

                    int i = cmd.ExecuteNonQuery();

                    if (i == -1)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Approved Successfully";
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Approved Failed";
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

        public Response GetListOfUsers(RegisterUser registerUser, SqlConnection conn)
        {
            Response response = new Response();
            List<RegisterUser> registeredUser = new List<RegisterUser>();
            try
            {
                using(SqlCommand cmd = new SqlCommand("GET_LIST_OF_USERS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        response.StatusCode = 200;
                        response.StatusMessage = "Able to get list of users";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RegisterUser reg = new RegisterUser();
                            reg.ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                            reg.Name = Convert.ToString(dt.Rows[i]["Name"]);
                            reg.PhoneNo = Convert.ToString(dt.Rows[i]["PhoneNo"]);
                            reg.Email = Convert.ToString(dt.Rows[i]["Email"]);
                            reg.Role = Convert.ToInt32(dt.Rows[i]["Role"]);
                            reg.IsApproved = Convert.ToInt32(dt.Rows[i]["IsApproved"]);

                            registeredUser.Add(reg);
                            if (registeredUser.Count > 0)
                            {
                                response.ListUser = registeredUser;
                                response.StatusCode = 200;
                                response.StatusMessage = "Able to get list of users";
                            }
                            else
                            {
                                response.StatusCode = 100;
                                response.StatusMessage = "Unable to get the list of users";
                                response.RegistrationUser = null;
                            }
                        }
                    }
                    else
                    {
                        response.StatusCode = 100;
                        response.StatusMessage = "Unable to get the list of users";
                        response.RegistrationUser = null;
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return response;
        }
    }
}
