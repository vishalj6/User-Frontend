using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class CreateUsersModel : PageModel
    {
        private readonly WebApis _webApis;
        public SelectList? City { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        public string? confirmUser_password { get; set; }
        [BindProperty]
        public UserPerson Users { get; set; }

        public CreateUsersModel(WebApis webApis)
        {
            _webApis = webApis;
        }
        public async Task<ActionResult> OnGet()
        {
            await PopulateCitySelectListAsync();
            return Page();
        }

        public async Task<ActionResult> OnPost()

        {
            if (!ModelState.IsValid)
            {
                await PopulateCitySelectListAsync();
                return Page();
            }

            if (Users.password != confirmUser_password)
            {
                TempData["isSuccess"] = false;
                TempData["SuccessMSG"] = "Password and Confirm Password do not match!";
                await PopulateCitySelectListAsync();
                return Page();
            }

            string otp = await _webApis.SendOtpApiAsync(Users.email, Users.firstName);
            TempData["OTP"] = otp;
            TempData["User"] = JsonConvert.SerializeObject(Users);
            TempData["UserPassword"] = Users.password;
            return RedirectToPage("./OtpVerification");
        }

        private async Task PopulateCitySelectListAsync()
        {
            City = new SelectList(await _webApis.GetCitiesApiAsync());
        }

    }
}
