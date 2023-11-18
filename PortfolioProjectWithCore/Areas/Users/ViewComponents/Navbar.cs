using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioProjectWithCore.Areas.Users.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly UserManager<AreaUser> _userManager;

        public Navbar(UserManager<AreaUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.v = values.ImageUrl;
            return View();
        }
    }
}
