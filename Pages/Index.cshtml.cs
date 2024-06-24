using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace Users.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WebApis _webApis = new();

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string password { get; set; }



        public void OnGet()
        {
            TempData.Clear();
        }
        public async Task<ActionResult> OnPostAsync()
        {
            //(int, string) isValidUserID = objUser.ValidateUserAsync(email, password);
            var validateUserRequest = new ValidateUserRequest
            {
                UserName = email,
                Password = password
            };

            var response = await _webApis.UserVaidationApiAsync(validateUserRequest);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                try
                {
                    (int, string) isValidUserID = (Convert.ToInt32(result["userId"]), result["firstName"].ToString());
                    var token = result["token"].ToString();
                    //string
                    if (isValidUserID.Item1 != 0)
                    {
                        Response.Cookies.Append("jwt_token", token, new CookieOptions
                        {
                            HttpOnly = false,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddHours(1)
                        });
                        TempData.Clear();
                        TempData["isUser"] = true;
                        TempData["UserId"] = isValidUserID.Item1.ToString();
                        TempData["UserName"] = isValidUserID.Item2.ToString();
                        TempData["UserPassword"] = password;
                        //return RedirectToPage("./UserIndex");
                        return RedirectToPage("./UserFolder/UserDetails", new { id = isValidUserID.Item1 });
                    }
                    else
                    {
                        TempData.Clear();
                        TempData["isSuccess"] = false;
                        TempData["SuccessMSG"] = "Username or Password is invalid!!";
                        return Page();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("An Error Occured in API");
            }
        }
    }
}