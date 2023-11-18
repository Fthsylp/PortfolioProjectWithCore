using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{

    public class ServiceController : Controller
    {
        ServiceManager serviceManager = new ServiceManager(new EfServiceDal());
        public IActionResult Index()
        {
            var values = serviceManager.TGetList();
            return View(values);
        }
        public IActionResult AddService() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddService(Service p) 
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
            serviceManager.TAdd(p);
            return RedirectToAction("Index");
        }
        public IActionResult EditService(int id) 
        {
            var value = serviceManager.TGetByID(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditService(Service p) 
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
                Service existingService = serviceManager.TGetByID(p.ServiceID); // Assuming Id is the unique identifier
                p.ImageUrl = existingService.ImageUrl;
            }
            serviceManager.TUpdate(p);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteService(int id) 
        {
            var value = serviceManager.TGetByID(id);
            serviceManager.TDelete(value);
            return RedirectToAction("Index");
        }
    }
}


