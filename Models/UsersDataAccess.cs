//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.Data;
//using System.Data.SqlClient;
//using System.Net.Mail;
//using System.Net;
//using System.Reflection.PortableExecutable;
//using System.Text.RegularExpressions;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Newtonsoft.Json;
//using System.Net.Http;

//namespace UsersProject.Models
//{
//    public class UsersDataAccess
//    {

//        //string connectionString = "data source=PC133-PC\\SQLEXPRESS; database=CONTACT_TASK2; integrated security=SSPI";
//        static DatabaseConnection newconn = new();
//        private static readonly DatabaseConnection sqlconn = newconn;

//        //public IEnumerable<UserPerson> ViewAllUsers(int currentPage, int maxRows, PaginationUser pageModel, List<string> UserSearch)
//        //{
//        //    string? firstName = UserSearch[0];
//        //    string? lastName = UserSearch[1];
//        //    string? phoneNo = UserSearch[2];
//        //    string? Email = UserSearch[3];
//        //    string? Country = UserSearch[4];
//        //    string? State = UserSearch[5];
//        //    string? City = UserSearch[6];
//        //    List<UserPerson> lstUsers = new();
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        //SqlCommand command = new("SELECT * FROM USER_TABLE_VIEW", conn);
//        //        SqlCommand command = new SqlCommand("FILTER_USER", conn);
//        //        command.CommandType = CommandType.StoredProcedure;
//        //        if (!String.IsNullOrEmpty(firstName))
//        //        {
//        //            command.Parameters.AddWithValue("@First_name", firstName);
//        //        }
//        //        if (!String.IsNullOrEmpty(lastName))
//        //        {
//        //            command.Parameters.AddWithValue("@Last_name", lastName);
//        //        }
//        //        if (!String.IsNullOrEmpty(phoneNo))
//        //        {
//        //            command.Parameters.AddWithValue("@phone_no", phoneNo);
//        //        }
//        //        if (!String.IsNullOrEmpty(Email))
//        //        {
//        //            command.Parameters.AddWithValue("@email", Email);
//        //        }
//        //        if (!String.IsNullOrEmpty(Country))
//        //        {
//        //            command.Parameters.AddWithValue("@Country", Country);
//        //        }
//        //        if (!String.IsNullOrEmpty(State))
//        //        {
//        //            command.Parameters.AddWithValue("@State", State);
//        //        }
//        //        if (!String.IsNullOrEmpty(City))
//        //        {
//        //            command.Parameters.AddWithValue("@City", City);
//        //        }
//        //        command.Parameters.AddWithValue("@PageNumber", currentPage);
//        //        command.Parameters.AddWithValue("@Limit", maxRows);
//        //        SqlDataReader sdr = command.ExecuteReader();

//        //        if (sdr.HasRows)
//        //        {
//        //            while (sdr.Read())
//        //            {
//        //                UserPerson Users = new()
//        //                {
//        //                    userID = Convert.ToInt32(sdr["person_id"]),
//        //                    firstName = sdr["First_name"].ToString(),
//        //                    lastName = sdr["Last_name"].ToString(),
//        //                    phoneNumber = sdr["Phone_no"].ToString(),
//        //                    country= sdr["Country_name"].ToString(),
//        //                    state= sdr["State_name"].ToString(),
//        //                    city= sdr["city_name"].ToString(),
//        //                    email = sdr["email"].ToString(),
//        //                    address = sdr["address_col"].ToString()
//        //                };
//        //                lstUsers.Add(Users);
//        //            }
//        //        }

//        //        if (sdr.NextResult())
//        //        {
//        //            if (sdr.HasRows)
//        //            {
//        //                sdr.Read();
//        //                pageModel.total_pages = Convert.ToInt32(sdr["TotalCount"]);
//        //                pageModel.total_users = Convert.ToInt32(sdr["TotalUsers"]);
//        //            }
//        //        }
//        //        sdr.Close();
//        //        return lstUsers;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return lstUsers;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}


//        //public List<string> Insert_User(UserPerson User)
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    int user_id = 0;
//        //    try
//        //    {
//        //        if (!Regex.IsMatch(User.email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
//        //        {
//        //            return ["false", "Email is not valid!!"];
//        //        }

//        //        string pattern = @"^\d{10}$";
//        //        if (!Regex.IsMatch(User.phoneNumber, pattern))
//        //        {
//        //            return ["false", "Phone no is not valid!!"];
//        //        }

//        //        SqlCommand cmd = new SqlCommand("InsertPersonData", conn);
//        //        cmd.CommandType = CommandType.StoredProcedure;

//        //        cmd.Parameters.AddWithValue("@First_name", User.firstName);
//        //        cmd.Parameters.AddWithValue("@Last_name", User.lastName);
//        //        cmd.Parameters.AddWithValue("@Phone_no", User.phoneNumber);
//        //        cmd.Parameters.AddWithValue("@city_name", User.city);
//        //        cmd.Parameters.AddWithValue("@email", User.email);
//        //        cmd.Parameters.AddWithValue("@address_col", User.address);
//        //        cmd.Parameters.AddWithValue("@User_password", User.password);

//        //        SqlDataReader sdr = cmd.ExecuteReader();
//        //        if (sdr.NextResult())
//        //        {
//        //            if (sdr.HasRows)
//        //            {
//        //                sdr.Read();
//        //                user_id = Convert.ToInt32(sdr["UserId"]);
//        //            }
//        //        }

//        //        if (user_id != 0)
//        //        {
//        //            return new List<string> { "true", "User Inserted Successfully", user_id.ToString(), User.firstName };
//        //        }
//        //        return ["false", "An error occurred."];
//        //    }
//        //    catch (SqlException e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        if (e.Number == 2627 || e.Number == 50000) // error code for unique key violation
//        //        {
//        //            return ["false", "Duplicate entry detected.(Phone no or Email)"];
//        //        }
//        //        else
//        //        {
//        //            return ["false", "An error occurred."];
//        //        }
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}




//        //public List<string> UpdateUsers(UserPerson? User = null, int? UserId = 0, string? password = "")
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        SqlCommand cmd = new SqlCommand("UPDATE_USER", conn);
//        //        cmd.CommandType = CommandType.StoredProcedure;

//        //        if (User == null)
//        //        {
//        //            if (UserId != 0 && !string.IsNullOrEmpty(password))
//        //            {
//        //                cmd.Parameters.AddWithValue("@Update_id", UserId);
//        //                cmd.Parameters.AddWithValue("@Update_pas", password);
//        //                SqlDataReader sdr = cmd.ExecuteReader();
//        //                if (sdr.HasRows)
//        //                {
//        //                    sdr.Read();
//        //                    string username = sdr["First_name"].ToString();
//        //                    return ["true", "Updated Successfully", UserId.ToString(), username];
//        //                }
//        //                else
//        //                {
//        //                    return ["false", "An error occurred."];
//        //                }
//        //            }
//        //            return ["false", "An error occurred."];

//        //        }
//        //        else
//        //        {
//        //            if (!Regex.IsMatch(User.email, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
//        //            {
//        //                return ["false", "Email is not valid!!"];

//        //            }
//        //            string pattern = @"^\d{10}$";
//        //            if (!Regex.IsMatch(User.phoneNumber, pattern))
//        //            {
//        //                return ["false", "Phone no is not valid!!"];
//        //            }

//        //            cmd.Parameters.AddWithValue("@Update_id", User.userID);
//        //            cmd.Parameters.AddWithValue("@First_name", User.firstName);
//        //            cmd.Parameters.AddWithValue("@Last_name", User.lastName);
//        //            cmd.Parameters.AddWithValue("@Phone_no", User.phoneNumber);
//        //            cmd.Parameters.AddWithValue("@city_name", User.city);
//        //            cmd.Parameters.AddWithValue("@email", User.email);
//        //            cmd.Parameters.AddWithValue("@Address", User.address);

//        //            int rowAff = cmd.ExecuteNonQuery();
//        //            if (rowAff > 0)
//        //            {
//        //                return ["true", "Updated Successfully", User.userID.ToString(), User.firstName.ToString()];
//        //            }
//        //            else
//        //            {
//        //                return ["false", "An error occurred."];
//        //            }
//        //        }
//        //    }
//        //    catch (SqlException e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        if (e.Number == 2627) // error code for unique key violation
//        //        {
//        //            return ["false", "Duplicate entry detected. (Phone no or Email)"];
//        //        }
//        //        else
//        //        {
//        //            return ["false", "An error occurred."];
//        //        }
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}




//        //SqlConnection conn = sqlconn.GetConnection();
//        //try
//        //{
//        //    SqlCommand cmd = new("FIND_USER", conn)
//        //    {
//        //        CommandType = System.Data.CommandType.StoredProcedure
//        //    };
//        //    cmd.Parameters.AddWithValue("@USER_ID", user_id);
//        //    if (user_password != "0")
//        //    {
//        //        cmd.Parameters.AddWithValue("@User_password", user_password);
//        //    }
//        //    SqlDataReader sdr = cmd.ExecuteReader();

//        //    while (sdr.Read())
//        //    {
//        //        User.userID = Convert.ToInt32(sdr["person_id"]);
//        //        User.firstName = sdr["First_name"].ToString();
//        //        User.lastName = sdr["Last_name"].ToString();
//        //        User.phoneNumber = sdr["Phone_no"].ToString();
//        //        User.country = sdr["Country_name"].ToString();
//        //        User.state = sdr["State_name"].ToString();
//        //        User.city = sdr["city_name"].ToString();
//        //        User.email = sdr["email"].ToString();
//        //        User.address = sdr["address_col"].ToString();
//        //        //User.password = sdr["User_password"].ToString();
//        //    }
//        //    return User;
//        //}
//        //catch (Exception e)
//        //{
//        //    Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //    return User;
//        //}
//        //finally
//        //{
//        //    conn.Close();
//        //}
//        //}



//        //public List<string> DeleteUsers(int? delete_id)
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        SqlCommand command = new("DeletePersonData", conn)
//        //        {
//        //            CommandType = System.Data.CommandType.StoredProcedure
//        //        };
//        //        command.Parameters.AddWithValue("@deleteid", delete_id);
//        //        command.ExecuteNonQuery();
//        //        return ["true", "Successful"];
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return ["false", "Error occured"];
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}

//        //public List<string> GetCountries()
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    List<string> countryList = [];
//        //    try
//        //    {
//        //        SqlCommand command = new("SELECT (country_name) FROM COUNTRIES", conn);
//        //        SqlDataReader sdr = command.ExecuteReader();
//        //        while (sdr.Read())
//        //        {
//        //            string onecountry = sdr["country_name"].ToString();
//        //            countryList.Add(onecountry);
//        //        }
//        //        return countryList;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return countryList;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}
//        //public List<string> GetStates()
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    List<string> stateList = [];
//        //    try
//        //    {
//        //        SqlCommand command = new("SELECT (state_name) FROM STATE_TABLE_VIEW", conn);
//        //        SqlDataReader sdr = command.ExecuteReader();
//        //        while (sdr.Read())
//        //        {
//        //            string onestate = sdr["state_name"].ToString();
//        //            stateList.Add(onestate);
//        //        }
//        //        return stateList;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return stateList;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}
//        //public List<string> GetCities()
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    List<string> cityList = [];
//        //    try
//        //    {
//        //        SqlCommand command = new("SELECT (city_name) FROM CITY_TABLE_VIEW", conn);
//        //        SqlDataReader sdr = command.ExecuteReader();
//        //        while (sdr.Read())
//        //        {
//        //            string onecity = sdr["city_name"].ToString();
//        //            cityList.Add(onecity);
//        //        }
//        //        return cityList;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return cityList;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}


//        //public async Task<(int, string)> ValidateUserAsync(string email, string login_password = "")
//        //{
//        //        SqlConnection conn = sqlconn.GetConnection();
//        //    UserPerson User = new();
//        //    try
//        //    {
//        //        SqlCommand command = new SqlCommand("FILTER_USER", conn);
//        //        command.CommandType = CommandType.StoredProcedure;
//        //        command.Parameters.AddWithValue("@Email", email);
//        //        command.Parameters.AddWithValue("@User_password", login_password);
//        //        SqlDataReader sdr = command.ExecuteReader();

//        //        if (sdr.HasRows)
//        //        {
//        //            await sdr.ReadAsync();
//        //            User.userID = Convert.ToInt32(sdr["person_id"]);
//        //            User.firstName = sdr["First_name"].ToString();
//        //            //User.lastName = sdr["Last_name"].ToString();
//        //            //User.phoneNumber = sdr["Phone_no"].ToString();
//        //            //User.email = sdr["email"].ToString();
//        //            //User.password = sdr["User_password"].ToString();
//        //            return (User.userID, User.firstName);
//        //        }
//        //        return (0, "");

//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return (0, "");
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}

//        //public bool Admin_Validation(string admin_username, string admin_pas)
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        SqlCommand command = new SqlCommand("ValidateAdmin", conn);
//        //        command.CommandType = CommandType.StoredProcedure;
//        //        command.Parameters.AddWithValue("@admin_username", admin_username);
//        //        command.Parameters.AddWithValue("@admin_password", admin_pas);
//        //        SqlDataReader sdr = command.ExecuteReader();
//        //        if (sdr.HasRows)
//        //        {
//        //            while (sdr.Read())
//        //            {
//        //                if (sdr["Result"].ToString() == "Admin exists")
//        //                {
//        //                    return true;
//        //                }
//        //            }
//        //            return false;
//        //        }
//        //        else
//        //        {
//        //            return false;
//        //        }
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return false;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}


//        //        public string SendOTPEmail(string userEmail, string FirstName)
//        //        {
//        //            Random random = new Random();
//        //            string otp = random.Next(100000, 999999).ToString();
//        //            var fromAddress = new MailAddress("vishaljadeja6@gmail.com", "Vishal Jadeja");
//        //            var toAddress = new MailAddress(userEmail);
//        //            string fromPassword = "rekd jfoe egod dxbz"; // Replace with your generated App Password
//        //            const string subject = "Your OTP Code";
//        //            string body = $@"<html lang=""en"">
//        //  <head>
//        //    <meta charset=""UTF-8"" />
//        //    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
//        //    <title>OTP Verification</title>
//        //  </head>
//        //  <body style=""margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f7f7f7; text-align: center;"">
//        //    <div style=""min-height:400px;background-color: #ffffff; border-radius: 10px; box-shadow: 0px 10px 20px rgba(0, 0, 0, 0.5); border: 1px solid #00000050; padding: 0px; text-align: center; max-width: 400px; margin: auto; width: 100%;"">
//        //      <div style=""background-color: #0b335d; border-radius: 10px 10px 0 0; color: #ffffff; padding: 20px; font-size: 1.5rem; font-weight: bold; margin-bottom: 20px;"">OTP Verification</div>
//        //      <h1 style=""color: #0b335d; font-size: 1.5rem; margin-bottom: 20px;"">Hello {FirstName}!</h1>
//        //      <div style=""font-size: 3rem; margin-bottom: 20px; animation: bounce 0.5s infinite alternate;"">&#128274;</div>
//        //      <p style=""color: #333; margin: 0 10px; margin-bottom: 20px;"">Your One-Time Password (OTP) is:</p>
//        //      <div style=""font-size: 2rem; font-weight: bold; color: #ff5733; margin-bottom: 20px;"">{otp}</div>
//        //      <p style=""color: #666; font-size: 0.8rem; margin-bottom: 10px;"">If you didn't request this, ignore this email.</p>
//        //      <p style=""margin-top: 20px; margin-bottom: 30px; color: #0b335d; font-weight: bold;"">Best regards,<br />Vishal Jadeja</p>
//        //    </div>
//        //  </body>
//        //</html>";

//        //            var smtp = new SmtpClient
//        //            {
//        //                Host = "smtp.gmail.com",
//        //                Port = 587,
//        //                EnableSsl = true,
//        //                DeliveryMethod = SmtpDeliveryMethod.Network,
//        //                UseDefaultCredentials = false,
//        //                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
//        //            };

//        //            using (var message = new MailMessage(fromAddress, toAddress)
//        //            {
//        //                Subject = subject,
//        //                Body = body,
//        //                IsBodyHtml = true
//        //            })
//        //            {
//        //                smtp.Send(message);
//        //            }
//        //            return otp;
//        //        }


//        //public bool DeleteAllPersons()
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        SqlCommand command = new("DELETE_ALL_USERS", conn);
//        //        command.CommandType = CommandType.StoredProcedure;
//        //        int rowAff = command.ExecuteNonQuery();
//        //        if (rowAff > 0)
//        //        {
//        //            return true;
//        //        }
//        //        else
//        //        {
//        //            return false;
//        //        }
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return false;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}

//        //public int ValidateUserByEmail(string userEmail)
//        //{
//        //    SqlConnection conn = sqlconn.GetConnection();
//        //    try
//        //    {
//        //        int person_id;
//        //        SqlCommand command = new("FIND_USER", conn)
//        //        {
//        //            CommandType = System.Data.CommandType.StoredProcedure
//        //        };
//        //        command.Parameters.AddWithValue("@Email", userEmail);
//        //        SqlDataReader sdr = command.ExecuteReader();
//        //        if (sdr.HasRows)
//        //        {
//        //            sdr.Read();
//        //            person_id = Convert.ToInt32(sdr["person_id"]);
//        //            return person_id;
//        //        }
//        //        else
//        //        {
//        //            return 0;
//        //        }
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        Console.WriteLine("\nOOPs, something went wrong.\n" + e.Message);
//        //        return 0;
//        //    }
//        //    finally
//        //    {
//        //        conn.Close();
//        //    }
//        //}

//    }
//}