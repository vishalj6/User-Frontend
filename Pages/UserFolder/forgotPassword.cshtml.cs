using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UsersProject.ApiSevices;

namespace UsersProject.Pages.UserFolder
{
    public class forgotPasswordModel : PageModel
    {
        private readonly WebApis _webApis = new();

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string userEmail { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string confirmPassword { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (userEmail != null)
            {
                //if (objUser.ValidateUserByEmail(userEmail) != 0)
                if (await _webApis.UserVaidationByMailApiAsync(userEmail) != 0)
                {
                    string otp = await _webApis.SendOtpApiAsync(userEmail, "");
                    //string otp = objUser.SendOTPEmail(userEmail, "");
                    TempData["OTP"] = otp;

                    TempData["UserId"] = await _webApis.UserVaidationByMailApiAsync(userEmail);
                    //TempData["UserId"] = objUser.ValidateUserByEmail(userEmail);

                    return RedirectToPage("./OtpVerification", new { isResetPassword = "true" });
                }
                else
                {
                    TempData["isSuccess"] = false;
                    TempData["SuccessMSG"] = "User does not exist!!";
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostResetPasswordAsync()
        {
            if (Password == confirmPassword)
            {
                int UserId = 0;
                if (TempData["UserId"] != null)
                {
                    UserId = Convert.ToInt32(TempData["UserId"]);
                }
                if (UserId != 0)
                {
                    var response = await _webApis.UserUpdateApiAsync(UserId: UserId, Password: Password);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<string>>(responseContent);

                        var isSuccess = Convert.ToBoolean(result[0]);
                        var successMsg = Convert.ToString(result[1]);

                        TempData["isSuccess"] = isSuccess;
                        TempData["SuccessMSG"] = successMsg;

                        var isUser = TempData["isUser"] as bool? ?? false;
                        TempData.Keep("isUser");

                        if (isUser)
                        {
                            return RedirectToPage("./UserDetails", new { id = result[2] });
                        }

                        return RedirectToPage("./UserIndex");
                    }
                    else
                    {
                        TempData["isSuccess"] = false;
                        TempData["SuccessMSG"] = "Error occurred while updating user.";
                        return RedirectToPage("./CreateUsers");
                    }
                }
                else
                {
                    TempData["isSuccess"] = false;
                    TempData["SuccessMSG"] = "User does not exist!";
                }
            }
            else
            {
                TempData["isSuccess"] = false;
                TempData["SuccessMSG"] = "Password and confirm password do not match!";
            }
            return Page();
        }
    }
}
