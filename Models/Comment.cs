namespace emlak_son.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public int PropertyId { get; set; }
    public Property? Property { get; set; }
}
