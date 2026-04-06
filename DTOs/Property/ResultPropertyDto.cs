namespace emlak_son.DTOs.Property;

public class ResultPropertyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int SquareMeters { get; set; }
    public int RoomCount { get; set; }
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CategoryId { get; set; }
    public int AppUserId { get; set; }
}
