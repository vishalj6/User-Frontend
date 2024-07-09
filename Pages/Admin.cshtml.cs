using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Middleware;
using UsersProject.Models;

namespace UsersProject.Pages
{
    public class AdminModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string adminname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string adminpas { get; set; }

        private readonly WebApis _webApis;

        public AdminModel(WebApis webApis)
        {
            _webApis = webApis;
        }
        public void OnGet()
        {
            Response.Cookies.Delete("jwt_token");
            TempData.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var validateUserRequest = new ValidateUserRequest
            {
                UserName = adminname,
                Password = adminpas
            };
            //bool isValidUserID = objUser.Admin_Validation(adminname, adminpas);

            var response = await _webApis.AdminVaidationApiAsync(validateUserRequest);
            if (response.IsSuccessStatusCode)
            {
                string returnedToken = DecryptionHelper.DecryptString(await response.Content.ReadAsStringAsync());
                var returnedTokenString = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnedToken);
                Response.Cookies.Append("jwt_token", returnedTokenString["token"].ToString(), new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });
                TempData.Clear();
                TempData["isAdmin"] = true;
                TempData["AdminName"] = adminname;
                TempData["AdminNameForDelete"] = adminname;
                //TempData.Keep("AdminName");
                return RedirectToPage("/UserFolder/UserIndex");
            }
            else
            {
                TempData.Clear();
                TempData["isSuccess"] = false;
                TempData["SuccessMSG"] = "Adminname or Password is invalid!!";
                return Page();
                //return BadRequest("Admin not found !! Error in API");
            }

        }
    }
}
