using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
