using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class PortfolioController : Controller
    {
        PortfolioManager portfolioManager = new PortfolioManager(new EfPortfolioDal());
        public IActionResult Index()
        {
            var values = portfolioManager.TGetList();
            return View(values);
        }
        public IActionResult AddPortfolio()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPortfolio(Portfolio p)
        {
            PortfolioValidatior validations = new PortfolioValidatior();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    IFormFile imageFile = HttpContext.Request.Form.Files[0];
                    string imageFilename = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string imageExtension = Path.GetExtension(imageFile.FileName);
                    string imageFilePath = Path.Combine("wwwroot", "Image", imageFilename + imageExtension);

                    using (FileStream stream = new FileStream(imageFilePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    p.ImageUrl = "/Image/" + imageFilename + imageExtension;

                    if (HttpContext.Request.Form.Files.Count > 1)
                    {
                        IFormFile biggerImageFile = HttpContext.Request.Form.Files[1];
                        string biggerImageFilename = Path.GetFileNameWithoutExtension(biggerImageFile.FileName);
                        string biggerImageExtension = Path.GetExtension(biggerImageFile.FileName);
                        string biggerImageFilePath = Path.Combine("wwwroot", "Image", biggerImageFilename + biggerImageExtension);

                        using (FileStream stream = new FileStream(biggerImageFilePath, FileMode.Create))
                        {
                            biggerImageFile.CopyTo(stream);
                        }

                        p.BiggerImageUrl = "/Image/" + biggerImageFilename + biggerImageExtension;
                    }
                }

                portfolioManager.TAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        public IActionResult DeletePortfolio(int id)
        {
            var value = portfolioManager.TGetByID(id);
            portfolioManager.TDelete(value);
            return RedirectToAction("Index");
        }
        public IActionResult EditPortfolio(int id)
        {
            var values = portfolioManager.TGetByID(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditPortfolio(Portfolio p)
        {
            PortfolioValidatior validations = new PortfolioValidatior();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0 && !string.IsNullOrEmpty(HttpContext.Request.Form.Files[0].FileName))
                {
                    IFormFile imageFile = HttpContext.Request.Form.Files[0];
                    string imageFilename = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string imageExtension = Path.GetExtension(imageFile.FileName);
                    string imageFilePath = Path.Combine("wwwroot", "Image", imageFilename + imageExtension);

                    using (FileStream stream = new FileStream(imageFilePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    p.ImageUrl = "/Image/" + imageFilename + imageExtension;

                    if (HttpContext.Request.Form.Files.Count > 1 && !string.IsNullOrEmpty(HttpContext.Request.Form.Files[1].FileName))
                    {
                        IFormFile biggerImageFile = HttpContext.Request.Form.Files[1];
                        string biggerImageFilename = Path.GetFileNameWithoutExtension(biggerImageFile.FileName);
                        string biggerImageExtension = Path.GetExtension(biggerImageFile.FileName);
                        string biggerImageFilePath = Path.Combine("wwwroot", "Image", biggerImageFilename + biggerImageExtension);

                        using (FileStream stream = new FileStream(biggerImageFilePath, FileMode.Create))
                        {
                            biggerImageFile.CopyTo(stream);
                        }

                        p.BiggerImageUrl = "/Image/" + biggerImageFilename + biggerImageExtension;


                    }
                    else 
                    {
                        Portfolio existingPortfolio = portfolioManager.TGetByID(p.PortfolioID);
                        p.BiggerImageUrl = existingPortfolio.BiggerImageUrl;
                    }
                }
                else
                {
                    Portfolio existingPortfolio = portfolioManager.TGetByID(p.PortfolioID);
                    p.ImageUrl = existingPortfolio.ImageUrl;
                    p.BiggerImageUrl = existingPortfolio.BiggerImageUrl;
                }

                portfolioManager.TUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
    }
}
