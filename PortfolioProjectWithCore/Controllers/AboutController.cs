using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class AboutController : Controller
    {
        readonly AboutManager aboutManager = new AboutManager(new EfAboutDal());
        public IActionResult Index()
        {
            var value = aboutManager.TGetByID(1);
            return View(value);
        }
        [HttpPost]
        public IActionResult Index(About p)
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
                About existingAbout = aboutManager.TGetByID(p.AboutId); // Assuming Id is the unique identifier
                p.ImageUrl = existingAbout.ImageUrl;
            }
            aboutManager.TUpdate(p);
            return RedirectToAction("Index", "Default");
        }
    }
}



