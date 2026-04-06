using Microsoft.AspNetCore.Identity;

namespace emlak_son.Models;

public class AppUser : IdentityUser<int>
{
    public ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
