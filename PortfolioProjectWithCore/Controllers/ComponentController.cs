using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class ComponentController : Controller
    {
        public PartialViewResult PartialFooter()
        {
            return PartialView();
        }
        public PartialViewResult PartialNavbar() 
        {
            return PartialView();
        }
        public PartialViewResult Head()
        {
            return PartialView();
        }
        public PartialViewResult Script()
        {
            return PartialView();
        }
    }
}
