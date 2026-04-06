using System.ComponentModel.DataAnnotations;

namespace emlak_son.DTOs.PropertyImage;

public class CreatePropertyImageDto
{
    [Required]
    [MaxLength(1000)]
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsMain { get; set; }
}
