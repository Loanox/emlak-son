namespace emlak_son.DTOs.Property;

public class PropertySearchDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    
    public string? City { get; set; }
    public string? District { get; set; }
    
    public int? MinRoomCount { get; set; }
    public int? MinSquareMeters { get; set; }
}
