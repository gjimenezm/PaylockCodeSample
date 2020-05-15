using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LockApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LockApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IList<User> _Users;
        


        static HomeController()
        {
            var userList = HomeController.GetUsers().Result;
            if (userList.Any())
            {
                _Users=userList;
            }
            else
            {
                _Users = new List<User>
            {
                new User
                {
                    Id = 0,
                    Name = "__",
                    LastName = "__"
                }
            };
            }
        }

        static async Task<List<User>> GetUsers()
        {
            List<User> userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44368/api/Users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
                return userList;
            }
        }

        static async Task<long> AddApi(User user)
        {
            
            using (var httpClient = new HttpClient())
            {
                User responseUser = new User();
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:44368/api/Users/", content))
                {
                    string apiResponse =  response.Content.ReadAsStringAsync().Result;
                    responseUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }

                return responseUser.Id;
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

           
                return View(_Users);
            


        }

        [Route("users")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Users()
        {
            return Json(_Users);
        }

        [Route("users/new")]
        [HttpPost]
        public ActionResult AddUser(User myUser)
        {
            var id =AddApi(myUser);
            // Create a fake ID for this User
            myUser.Id = _Users.Count + 1;

            _Users.Add(myUser);
            return Content("Success :)");
        }
    }
}
