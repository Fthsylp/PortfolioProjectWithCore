using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Concrete;

namespace PortfolioProjectWithCore.Areas.Users.Controllers
{
    [Area("Users")]
    public class DashboardUserController : Controller
    {
        private readonly UserManager<AreaUser> _userManager;

        public DashboardUserController(UserManager<AreaUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity?.Name);
            ViewBag.user = value.Name + " " + value.Surname;
            

            // Weather api

            string api = "e1aa773b7ff0c28fb55329eba4219a5b";
            string connection = "https://api.openweathermap.org/data/2.5/weather?q=Van&units=metric&appid=" + api;

            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(connection);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var weatherData = Newtonsoft.Json.Linq.JObject.Parse(json);
                Console.WriteLine(weatherData.ToString());
                ViewBag.v5 = (int)weatherData["main"]["temp"];
            }

            Context c = new Context();
            ViewBag.v1 = 0;
            ViewBag.v2 = c.Announcements.Count();
            ViewBag.v3 = 0;
            ViewBag.v4 = c.Skills.Count();
            return View();

        }
        
    }
}
