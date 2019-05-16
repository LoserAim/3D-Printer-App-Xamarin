using Newtonsoft.Json;
using PrintQue.Models;
using PrintQue.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Helper
{
    public class ApiHelper
    {
        public static async Task<bool> RegisterAsync(UserViewModel user)
        {
            var client = new HttpClient();
            var us = new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ConfirmPassword = user.confirmPassword,
                Email = user.Email,
                Password = user.Password,
            };
            var json = JsonConvert.SerializeObject(us);
            HttpContent content = new StringContent(json);
            //Need to add url for Account register page
            var response = await client.PostAsync("http://3dprintqueueweb.azurewebsites.net/Identity/Account/Register", content);
            return response.IsSuccessStatusCode;
        }
    }
}
