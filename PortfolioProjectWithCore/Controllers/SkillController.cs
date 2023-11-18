using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PortfolioProject.Controllers
{
    public class SkillController : Controller
    {
        SkillManager skillManager = new SkillManager(new EfSkillDal());
        public IActionResult Index()
        {
            var values = skillManager.TGetList();
            return View(values);
        }
        public IActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSkill(Skill p)
        {
            SkillValidatior validations = new SkillValidatior();
            ValidationResult results = validations.Validate(p);
            if (results.IsValid)
            {
                skillManager.TAdd(p);
                return RedirectToAction("Index");
            }

            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return View();
        }
        public IActionResult DeleteSkill(int id)
        {
            var value = skillManager.TGetByID(id);
            skillManager.TDelete(value);
            return RedirectToAction("Index");
        }
        public IActionResult EditSkill(int id)
        {
            var value = skillManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditSkill(Skill p)
        {
            SkillValidatior validations = new SkillValidatior();
            ValidationResult results = validations.Validate(p);
            if (results.IsValid)
            {
                skillManager.TUpdate(p);
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
