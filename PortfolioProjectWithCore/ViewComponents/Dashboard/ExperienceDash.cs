using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.ViewComponents.Dashboard
{
    public class ExperienceDash : ViewComponent
    {
        readonly ExperienceManager experienceManager = new ExperienceManager(new EfExperienceDal());
        public IViewComponentResult Invoke(bool showAll = false)
        {
            if (showAll)
            {
                var values = experienceManager.TGetList().ToList();
                return View(values);
            }
            else
            {
                var firstFiveProjects = experienceManager.TGetList().Take(5).ToList();
                return View(firstFiveProjects);
            }
        }
    }

}

