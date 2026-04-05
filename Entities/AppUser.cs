using Microsoft.AspNetCore.Identity;

namespace emlak_son.Entities;

public class AppUser : IdentityUser<int>
{
    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
