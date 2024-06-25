using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class EditUserModel : PageModel
    {
        private readonly WebApis _webApis = new();
        [BindProperty]
        public UserPerson User { get; set; }

        public SelectList? City { get; set; }

        public async Task<ActionResult> OnGet(int? id)
        {
            var isAdmin = TempData["isAdmin"] as bool? ?? false;
            var isUser = TempData["isUser"] as bool? ?? false;
            var userId = Convert.ToInt32(TempData["UserId"] ?? 0);
            TempData.Keep("isAdmin");
            TempData.Keep("isUser");
            TempData.Keep("UserId");

            if (isAdmin || (isUser && userId == id))
            {
                if (!id.HasValue)
                {
                    return NotFound();
                }
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    User = await _webApis.GetUserDataApiAsync(id, token: token);
                }
                else
                {
                    return Unauthorized();
                }
                //User = await GetUserById(id.Value);

                if (User == null)
                {
                    return NotFound();
                }

                TempData["OldUser"] = JsonConvert.SerializeObject(User);
                TempData.Keep("UserPassword");
                City = new SelectList(await _webApis.GetCitiesApiAsync(token));

                return Page();
            }
            else
            {
                return RedirectToPage("/Admin");
            }
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    City = new SelectList(await _webApis.GetCitiesApiAsync(token));
                }
                return Page();
            }

            var oldUser = JsonConvert.DeserializeObject<UserPerson>(TempData["OldUser"] as string);
            var emailChanged = oldUser?.email != User.email;
            var isAdmin = TempData["isAdmin"] as bool? ?? false;

            if (!emailChanged || isAdmin)
            {
                TempData.Keep("isAdmin");
                //var result = objUser.UpdateUsers(User);
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    var response = await _webApis.UserUpdateApiAsync(User, token: token);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<string>>(responseContent);
                        var isSuccess = Convert.ToBoolean(result[0]);
                        var successMsg = Convert.ToString(result[1]);
                        if (isSuccess)
                        {
                            TempData["isSuccess"] = isSuccess;
                            TempData["SuccessMSG"] = successMsg;
                        }
                        else
                        {
                            TempData["isSuccess"] = false;
                            TempData["SuccessMSG"] = "";
                        }
                    }
                    else
                    {
                        // Handle failed API call (e.g., log error)
                        TempData["isSuccess"] = false;
                        TempData["SuccessMSG"] = "Error occurred while updating user.";
                        return RedirectToPage("./UserIndex");
                    }
                    var isUser = TempData["isUser"] as bool? ?? false;
                    if (isUser)
                    {
                        TempData.Keep("isUser");
                        return RedirectToPage("./UserDetails", new { id = oldUser?.userID });
                    }

                    return RedirectToPage("./UserIndex");

                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                string otp = await _webApis.SendOtpApiAsync(User.email, User.firstName);
                //var otp = objUser.SendOTPEmail(User.email, User.firstName);
                TempData["OTP"] = otp;
                TempData["User"] = JsonConvert.SerializeObject(User);
                return RedirectToPage("./OtpVerification");
            }
        }
    }
}
