using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UsersProject.ApiSevices;
using UsersProject.Models;

namespace UsersProject.Pages.UserFolder
{
    public class UserIndexModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string firstName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string lastName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string phoneNo { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Country { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? State { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }
        [BindProperty(SupportsGet = true)]
        public int DropDownLimit { get; set; } = 5;
        [BindProperty(SupportsGet = true)]
        public int currentPageIndex { get; set; } = 1;

        //public FilterData allFilter { get; set; }

        public SelectList? CountryList { get; set; }
        public SelectList? StateList { get; set; }
        public SelectList? CityList { get; set; }



        public PaginationUser pageModel { get; set; }
        //public ActionResult OnGet()
        //{
        //    if (TempData["isAdmin"] != null && Convert.ToBoolean(TempData["isAdmin"]) == true)
        //    {
        //        //TempData["isAdmin"] = true;
        //        TempData.Keep("isAdmin");
        //        pageModel = GetCustomers(currentPageIndex);
        //        PopulateSelectLists();
        //        return Page();
        //    }

        //    return RedirectToPage("/Admin");

        //}

        private readonly WebApis _webApis = new();
        public List<UserPerson> Users { get; set; }

        public async Task<ActionResult> OnGet()
        {
            if (TempData["isAdmin"] != null && Convert.ToBoolean(TempData["isAdmin"]) == true)
            {
                TempData["isAdmin"] = true;
                TempData.Keep("isAdmin");
                //pageModel.AllUsers = users;
                //pageModel.total_pages = 3;
                pageModel = await GetCustomers();
                if (Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    await PopulateSelectListsAsync(token);
                }
                else
                {
                    return Unauthorized();
                }
                return Page();
            }

            return RedirectToPage("/Admin");

        }

        private async Task<PaginationUser> GetCustomers()
        {
            //pageModel.currentPageIndex = currentPage;
            //firstName = firstName?.Trim();
            //lastName= lastName?.Trim();
            //phoneNo= phoneNo?.Trim();
            //Email = Email?.Trim();
            //pageModel.LimitDropDown = DropDownLimit;

            //List<string> UserSearch = [firstName, lastName, phoneNo, Email, Country, State, City];

            FilterData allFilter = new FilterData
            {
                firstName = firstName,
                lastName = lastName,
                phoneNo = phoneNo,
                Email = Email,
                Country = Country,
                State = State,
                City = City,
                DropDownLimit = DropDownLimit,
                currentPageIndex = currentPageIndex
            };

            var responseData = await _webApis.GetUsersApiAsync(allFilter);
            PaginationUser pageModel = new PaginationUser
            {
                AllUsers = JsonConvert.DeserializeObject<List<UserPerson>>(responseData["users"].ToString()),
                total_pages = Convert.ToInt32(responseData["totalPages"]),
                total_users = Convert.ToInt32(responseData["totalUsers"])
            };
            //pageModel.AllUsers = objUser.ViewAllUsers(currentPage, DropDownLimit, pageModel, UserSearch).ToList();
            return pageModel;
        }

        private async Task PopulateSelectListsAsync(string token)
        {

            CountryList = new SelectList(await _webApis.GetCountriesApiAsync(token));
            StateList = new SelectList(await _webApis.GetStatesApiAsync(token));
            CityList = new SelectList(await _webApis.GetCitiesApiAsync(token));
            //CountryList = new SelectList(objUser.GetCountries());
            //StateList = new SelectList(objUser.GetStates());
            //CityList = new SelectList(objUser.GetCities());
        }
    }
}
