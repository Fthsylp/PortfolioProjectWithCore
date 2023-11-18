using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.ViewComponents.Dashboard
{
    public class PortfolioProjects : ViewComponent
    {
        readonly PortfolioManager portfolioManager = new PortfolioManager(new EfPortfolioDal());
        public IViewComponentResult Invoke(bool showAll = false)
        {
            if (showAll)
            {
                var values = portfolioManager.TGetList().ToList();
                return View(values);
            }
            else 
            {
                var firstFiveProjects = portfolioManager.TGetList().Take(5).ToList();
                return View(firstFiveProjects);
            }
        }
    }
}
