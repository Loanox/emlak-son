namespace emlak_son.Models;

public class Favorite
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public int PropertyId { get; set; }
    public Property? Property { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
