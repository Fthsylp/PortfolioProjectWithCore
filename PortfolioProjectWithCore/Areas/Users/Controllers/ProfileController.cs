using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioProjectWithCore.Areas.Users.Models;

namespace PortfolioProjectWithCore.Areas.Users.Controllers
{
    [Area("Users")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AreaUser> _userManager;

        public ProfileController(UserManager<AreaUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel model = new UserEditViewModel();
            model.Name = value.Name;
            model.Surname = value.Surname;
            model.Email = value.Email;
            model.PictureUrl = value.ImageUrl;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p) 
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Picture != null) 
            {
                var resource  = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Picture.FileName);
                var imagename = Guid.NewGuid() + extension;
                var saveLocation = resource + "/wwwroot/userImage/" + imagename;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await p.Picture.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.Name = p.Name;
            user.Surname = p.Surname;
            user.Email = p.Email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) 
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
