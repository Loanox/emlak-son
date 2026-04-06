namespace emlak_son.Models;

public class PropertyImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }

    public int PropertyId { get; set; }
    public Property? Property { get; set; }
}
