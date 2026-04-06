namespace emlak_son.DTOs.PropertyImage;

public class ResultPropertyImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMain { get; set; }
    public int PropertyId { get; set; }
}
