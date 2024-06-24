using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class UserDetailsModel : PageModel
    {

        private readonly WebApis _webApi = new();

        public UserPerson User { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            bool isUser = TempData["isUser"] != null && (bool)TempData["isUser"];
            TempData.Keep("isUser");
            string userPassword = TempData["UserPassword"]?.ToString();
            TempData.Keep("UserPassword");

            if (Request.Cookies.TryGetValue("jwt_token", out var token))
            {
                if (isUser && userPassword != null)
                {
                    User = await _webApi.GetUserDataApiAsync(id, userPassword, token);
                    //User = await _objUser.GetUserDataAsync(id.Value, userPassword);
                }
                else
                {
                    User = await _webApi.GetUserDataApiAsync(id, token:token);
                    //User = await _objUser.GetUserDataAsync(id.Value);
                }
            }
            else
            {
                return Unauthorized();
            }


            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
