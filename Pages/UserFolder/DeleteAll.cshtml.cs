using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class DeleteAllModel : PageModel
    {
        private readonly WebApis _webApis = new();
        public bool isDeleteAll { get; set; }
        public void OnGet()
        {
            TempData["isDeleteAll"] = false;
        }

        public async Task<ActionResult> OnPostConfirmAdminAsync(string password)
        {
            // Validate the password
            //bool isAdminValid = objUser.Admin_Validation(TempData["AdminNameForDelete"].ToString(), password);
            var validateUserRequest = new ValidateUserRequest
            {
                UserName = TempData["AdminNameForDelete"].ToString(),
                Password = password
            };

            var response = await _webApis.AdminVaidationApiAsync(validateUserRequest);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var isAdminValid = JsonConvert.DeserializeObject<bool>(responseContent);
                if (isAdminValid)
                {
                    TempData["isDeleteAll"] = true; // Set TempData to indicate confirmation
                }
                else
                {
                    TempData["isDeleteAll"] = false;
                    TempData["isSuccess"] = false;
                    TempData["SuccessMSG"] = "Wrong Password!!";
                }
            }

            TempData.Keep("isDeleteAll");
            TempData.Keep("AdminNameForDelete");
            return Page();
        }
        public async Task<ActionResult> OnPostDeleteAll()
        {
            return Page();
            if (TempData["isDeleteAll"] as bool? == true)
            {
                await _webApis.DeleteAllPersonsApiAsync();
                //objUser.DeleteAllPersons();
                TempData["isDeleteAll"] = false; // Reset TempData after operation
                TempData["isSuccess"] = true;
                TempData["SuccessMSG"] = "All Data Deleted Successfully!!";
            }
            else
            {
                TempData["isDeleteAll"] = false; // Reset TempData after operation
                TempData["isSuccess"] = false;
                TempData["SuccessMSG"] = "Data Deletion Failed!!";

            }

            return RedirectToPage("./UserIndex");
        }
    }
}

