namespace emlak_son.DTOs.Comment;

public class ResultCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public DateTime CreatedDate { get; set; }
    public int AppUserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int PropertyId { get; set; }
}
