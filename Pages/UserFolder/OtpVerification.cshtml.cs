using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class OtpVerificationModel : PageModel
    {
        private readonly WebApis _webApis = new();

        [BindProperty]
        public string Action { get; set; }

        [BindProperty]
        public string otpVerificationInput { get; set; }
        [BindProperty]
        public string isResetPassword { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            TempData.Keep("User");
            UserPerson user = null;
            if (isResetPassword != "true")
            {
                user = JsonConvert.DeserializeObject<UserPerson>(TempData["User"] as string);
            }
            if (Action == "Verify")
            {
                string savedOtp = TempData["OTP"] as string;
                TempData.Keep("OTP");
                if (otpVerificationInput == savedOtp && user != null)
                {
                    bool isSuccess;
                    string successMsg;
                    List<string> result;
                    //List<string> responseContent;
                    if (TempData["isUser"] != null && TempData["isUser"] as Boolean? == true)
                    {
                        TempData.Keep("isUser");

                        if (Request.Cookies.TryGetValue("jwt_token", out var token))
                        {
                            var response = await _webApis.UserUpdateApiAsync(user, token: token);
                            if (response.IsSuccessStatusCode)
                            {
                                string responseContent = await response.Content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<string>>(responseContent);
                                //result = objUser.UpdateUsers(user);
                            }
                            else
                            {
                                // Handle failed API call (e.g., log error)
                                TempData["isSuccess"] = false;
                                TempData["SuccessMSG"] = "Error occurred while Updating user.";
                                return RedirectToPage("./CreateUsers");
                            }
                            isSuccess = Convert.ToBoolean(result[0]);
                            successMsg = Convert.ToString(result[1]);
                        }
                        else
                        {
                            return Unauthorized();
                        }
                    }
                    else
                    {
                        //result = objUser.Insert_User(user);
                        if (Request.Cookies.TryGetValue("jwt_token", out var token))
                        {
                            var response = await _webApis.InsertUserApiAsync(user,token);

                            if (response.IsSuccessStatusCode)
                            {
                                string responseContent = await response.Content.ReadAsStringAsync();
                                result = JsonConvert.DeserializeObject<List<string>>(responseContent);
                                // Handle successful API response (e.g., parse response data)
                            }
                            else
                            {
                                // Handle failed API call (e.g., log error)
                                TempData["isSuccess"] = false;
                                TempData["SuccessMSG"] = "Error occurred while creating user.";
                                return RedirectToPage("./CreateUsers");
                            }

                            isSuccess = Convert.ToBoolean(result[0]);
                            successMsg = Convert.ToString(result[1]);
                        }
                        else
                        {
                            return Unauthorized();
                        }

                    }

                    if (isSuccess)
                    {
                        TempData["isSuccess"] = true;
                        TempData["SuccessMSG"] = "Operation Succesfull!";
                        if (TempData["isAdmin"] != null && Convert.ToBoolean(TempData["isAdmin"]) == true)
                        {
                            TempData.Keep("isAdmin");
                            return RedirectToPage("./UserIndex");
                        }
                        else
                        {
                            TempData["isUser"] = true;
                            TempData["UserId"] = result[2];
                            //TempData["UserName"] = result[3];
                            return RedirectToPage("./UserDetails", new { id = result[2] });
                        }
                    }
                    else
                    {
                        TempData["isSuccess"] = false;
                        TempData["SuccessMSG"] = successMsg;
                        return RedirectToPage("./CreateUsers");
                    }
                }
                else if (otpVerificationInput == savedOtp && isResetPassword == "true")
                {
                    TempData["ResetPasswordUserValidated"] = true;
                    return RedirectToPage("./forgotPassword");
                }
                else
                {
                    TempData["isSuccess"] = false;
                    TempData["SuccessMSG"] = "Invalid OTP!";
                    return Page();
                }
            }
            else if (Action == "Resend")
            {
                if (user != null)
                {
                    string otp = await _webApis.SendOtpApiAsync(user.email, user.firstName);
                    TempData["OTP"] = otp;
                }
                //return Page();
            }
            return Page();
        }
    }
}
