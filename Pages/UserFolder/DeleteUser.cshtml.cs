using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Middleware;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class DeleteUserModel : PageModel
    {
        private readonly WebApis _webApis;

        [BindProperty]
        public UserPerson User { get; set; }

        public DeleteUserModel(WebApis webApis)
        {
            _webApis = webApis;
        }
        public async Task<ActionResult> OnGet(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            if (TempData["isUser"] != null && TempData["isUser"] as Boolean? == true && TempData["UserPassword"] != null)
            {
                TempData.Keep("isUser");
                TempData.Keep("UserPassword");
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    User = await _webApis.GetUserDataApiAsync(id, TempData["UserPassword"].ToString(), token);
                    TempData.Keep("UserPassword");
                }
                else
                {
                    return Unauthorized();
                }
                //User = objUser.GetUserData(id, TempData["UserPassword"].ToString());
            }
            else
            {
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    User = await _webApis.GetUserDataApiAsync(id, token: token);
                }
                else
                {
                    return Unauthorized();
                }
                //User = objUser.GetUserData(id);
            }
            TempData.Keep("UserPassword");

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<ActionResult> OnPost(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (Request.Cookies.TryGetValue("jwt_token", out var token))
            {
                var response = await _webApis.DeleteOnePersonApiAsync(id, token);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = DecryptionHelper.DecryptString(await response.Content.ReadAsStringAsync());
                    var result = JsonConvert.DeserializeObject<List<string>>(responseContent);
                    // Handle successful API response (e.g., parse response data)
                    bool isSuccess = Convert.ToBoolean(result[0]);
                    string successMsg = Convert.ToString(result[1]);
                    //List<string> Result = objUser.DeleteUsers(id);
                    TempData["isSuccess"] = isSuccess;
                    TempData["SuccessMSG"] = successMsg;

                }
                else
                {
                    // Handle failed API call (e.g., log error)
                    TempData["isSuccess"] = false;
                    TempData["SuccessMSG"] = "Error occurred while Deleting user.";
                    return RedirectToPage("./UserIndex");
                }
            }
            else
            {
                return Unauthorized();
            }
            return RedirectToPage("./UserIndex");
        }
    }
}
