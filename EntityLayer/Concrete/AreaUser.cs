using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class AreaUser:IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
    }
}
