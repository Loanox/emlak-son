using System.ComponentModel.DataAnnotations;

namespace emlak_son.DTOs.Comment;

public class CreateCommentDto
{
    [Required]
    public int PropertyId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;
}
