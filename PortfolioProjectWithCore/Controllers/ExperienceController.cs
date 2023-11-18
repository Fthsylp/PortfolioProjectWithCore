using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;


namespace PortfolioProject.Controllers
{
    public class ExperienceController : Controller
    {
        ExperienceManager experienceManager = new ExperienceManager(new EfExperienceDal());
        public IActionResult Index()
        {
            var values = experienceManager.TGetList();
            return View(values);
        }
        public IActionResult AddExperience()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddExperience(Experience p)
        {
            ExperienceValidatior validations = new ExperienceValidatior();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    IFormFile file = HttpContext.Request.Form.Files[0];
                    string filename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string imgFilePath = Path.Combine("wwwroot", "Image", filename + extension);

                    using (FileStream stream = new FileStream(imgFilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    p.ImageUrl = "/Image/" + filename + extension;
                }
                experienceManager.TAdd(p);
                return RedirectToAction("Index");
            }

            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return View();
        }
        public IActionResult DeleteExperience(int id)
        {
            var value = experienceManager.TGetByID(id);
            experienceManager.TDelete(value);
            return RedirectToAction("Index");
        }
        public IActionResult EditExperience(int id)
        {
            var value = experienceManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditExperience(Experience p)
        {
            ExperienceValidatior validations = new ExperienceValidatior();
            ValidationResult results = validations.Validate(p);

            if (results.IsValid)
            {
                if (HttpContext.Request.Form.Files.Count > 0 && !string.IsNullOrEmpty(HttpContext.Request.Form.Files[0].FileName))
                {
                    IFormFile file = HttpContext.Request.Form.Files[0];
                    string filename = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string imgFilePath = Path.Combine("wwwroot", "Image", filename + extension);

                    using (FileStream stream = new FileStream(imgFilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    p.ImageUrl = "/Image/" + filename + extension;
                }
                else
                {
                    // Retain the existing ImageUrl value if no new file is uploaded
                    Experience existingExperience = experienceManager.TGetByID(p.ExperienceID); // Assuming Id is the unique identifier
                    p.ImageUrl = existingExperience.ImageUrl;
                }
                experienceManager.TUpdate(p);
                return RedirectToAction("Index");
            }
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return View();
        }
    }
}
