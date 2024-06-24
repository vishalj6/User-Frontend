using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UsersProject.Models;

namespace UsersProject.ApiSevices
{
    public class WebApis
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<Dictionary<string, object>> GetUsersApiAsync(FilterData allFilter)
        {
            var allFilter2 = JsonConvert.SerializeObject(allFilter);
            var response = await _httpClient.GetStringAsync($"https://localhost:7022/MainUser/GetUsers?allFilter={allFilter2}");
            var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            return responseData;
        }
        public async Task<UserPerson> GetUserDataApiAsync(int? user_id, string? user_password = "", string? token = "")
        {
            UserPerson User = new UserPerson();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetStringAsync($"https://localhost:7022/MainUser/GetUserData?UserId={user_id}&User_Password={user_password}");
            //var responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            //var ResUsers = JsonConvert.DeserializeObject<List<UserPerson>>(response);
            User = JsonConvert.DeserializeObject<UserPerson>(response.ToString());
            return User;
        }

        public async Task<HttpResponseMessage> UserUpdateApiAsync(UserPerson user = null, int UserId = 0, string? Password = "", string token="")
        {
            string apiUrl = $"https://localhost:7022/MainUser/UpdateUser?UserId={UserId}&password={Password}";
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<HttpResponseMessage> AdminVaidationApiAsync(ValidateUserRequest validateUserRequest)
        {
            string apiUrl = $"https://localhost:7022/MainUser/ValidateAdmin";
            var content = new StringContent(JsonConvert.SerializeObject(validateUserRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<HttpResponseMessage> UserVaidationApiAsync(ValidateUserRequest validateUserRequest)
        {
            string apiUrl = $"https://localhost:7022/MainUser/ValidateUser";
            var content = new StringContent(JsonConvert.SerializeObject(validateUserRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<int> UserVaidationByMailApiAsync(string userEmail)
        {
            UserPerson User = new();
            try
            {
                FilterData validateUser = new FilterData { Email = userEmail };
                var responseData = await GetUsersApiAsync(validateUser);
                User = JsonConvert.DeserializeObject<List<UserPerson>>(responseData["users"].ToString())[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
            return User.userID;
        }

        public async Task<HttpResponseMessage> InsertUserApiAsync(UserPerson user, string? token = "")
        {
            string apiUrl = "https://localhost:7022/MainUser/InsertUser";
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<string> SendOtpApiAsync(string userEmail, string? FirstName)
        {
            try
            {
                string apiUrl = $"https://localhost:7022/MainUser/SendOTP?userEmail={userEmail}&FirstName={FirstName}";
                var responseOTP = await _httpClient.GetStringAsync(apiUrl);
                return responseOTP;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }

        public async Task<List<string>> GetCountriesApiAsync(string token)
        {
            string apiUrl = $"https://localhost:7022/MainUser/GetCountries";
            // Retrieve the token from cookies
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetStringAsync(apiUrl);
            var responseData = JsonConvert.DeserializeObject<List<string>>(response);
            return responseData;
        }
        public async Task<List<string>> GetStatesApiAsync(string? token = "")
        {
            string apiUrl = $"https://localhost:7022/MainUser/GetStates";
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetStringAsync(apiUrl);
            var responseData = JsonConvert.DeserializeObject<List<string>>(response);
            return responseData;
        }
        public async Task<List<string>> GetCitiesApiAsync(string? token = "")
        {
            string apiUrl = $"https://localhost:7022/MainUser/GetCities";
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.GetStringAsync(apiUrl);
            var responseData = JsonConvert.DeserializeObject<List<string>>(response);
            return responseData;
        }
        public async Task<HttpResponseMessage> DeleteOnePersonApiAsync(int UserId)
        {

            string apiUrl = "https://localhost:7022/MainUser/DeleteUser"; // Replace with your actual API URL and controller method
            var content = new StringContent(JsonConvert.SerializeObject(UserId), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<bool> DeleteAllPersonsApiAsync()
        {
            string apiUrl = $"https://localhost:7022/MainUser/DeleteAllPersons";
            var response = await _httpClient.GetStringAsync(apiUrl);
            var responseData = JsonConvert.DeserializeObject<bool>(response);
            return responseData;
        }
    }
}
