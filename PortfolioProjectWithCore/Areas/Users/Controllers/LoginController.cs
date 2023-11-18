using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortfolioProjectWithCore.Areas.Users.Models;

namespace PortfolioProjectWithCore.Areas.Users.Controllers
{
    [Area("Users")]
    public class LoginController : Controller
    {
        public readonly SignInManager<AreaUser> _signInManager;

        public LoginController(SignInManager<AreaUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginViewModel p) 
        { 
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, true, true);
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password isn't correct");
                }
            }
            return View();
        }
    }
}
