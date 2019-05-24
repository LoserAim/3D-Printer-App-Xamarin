using Newtonsoft.Json;
using PrintQue.Models;
using PrintQue.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrintQue.Helper
{
    public class ApiHelper
    {
        public async Task<bool> RegisterAsync(UserViewModel user)
        {
            var client = new HttpClient();
            var us = new RegisterBindingModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ConfirmPassword = user.confirmPassword,
                Email = user.Email,
                Password = user.Password,
            };
            var json = JsonConvert.SerializeObject(us);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpContent content = new StringContent(json);
            HttpResponseMessage response;
            //Need to add url for Account register page
            string url = "http://3dprintqueueweb.azurewebsites.net/api/Account/Register";
            var uri = new Uri(url);
            response = await client.PostAsync(uri, content);
            //var response = await client.PostAsync(, content);
            return response.IsSuccessStatusCode;

        }
      
        public static async Task<bool> LoginAsync(UserViewModel user)
        {
            var client = new HttpClient();
            var us = new LoginBindingModel()
            {
                Email = user.Email,
                Password = user.Password,
            };
            var json = JsonConvert.SerializeObject(us);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpContent content = new StringContent(json);
            HttpResponseMessage response;
            //Need to add url for Account register page
            string url = "http://3dprintqueueweb.azurewebsites.net/api/Account/Login";
            var uri = new Uri(url);
            response = await client.PostAsync(uri, content);
            //var response = await client.PostAsync(, content);
            return response.IsSuccessStatusCode;

        }
    }
}
