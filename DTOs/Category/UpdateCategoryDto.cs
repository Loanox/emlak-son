using System.ComponentModel.DataAnnotations;

namespace emlak_son.DTOs.Category;

public class UpdateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    public bool Status { get; set; }
}
