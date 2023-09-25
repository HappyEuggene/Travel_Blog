using Microsoft.AspNetCore.Identity;

namespace Travel_Blog.Models;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Image? Image { get; set; }
}
