using System.ComponentModel.DataAnnotations;

namespace emlak_son.DTOs.Favorite;

public class CreateFavoriteDto
{
    [Required]
    public int PropertyId { get; set; }
}
