using System.ComponentModel.DataAnnotations;

namespace emlak_son.DTOs.Property;

public class CreatePropertyDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int SquareMeters { get; set; }

    [Range(1, int.MaxValue)]
    public int RoomCount { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string District { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}
