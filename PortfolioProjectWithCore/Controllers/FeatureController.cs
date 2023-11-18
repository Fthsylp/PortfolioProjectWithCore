using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class FeatureController : Controller
    {
        readonly FeatureManager featureManager = new FeatureManager(new EfFeatureDal());
        public IActionResult Index()
        {
            var value = featureManager.TGetByID(3);
            return View(value);
        }
        [HttpPost]
        public IActionResult Index(Feature p) 
        {
            featureManager.TUpdate(p);
            return RedirectToAction("Index", "Default");
        }

    }
}
