using System.ComponentModel.DataAnnotations;

namespace PortfolioProjectWithCore.Areas.Users.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
