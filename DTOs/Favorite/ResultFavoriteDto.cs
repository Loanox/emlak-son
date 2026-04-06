namespace emlak_son.DTOs.Favorite;

public class ResultFavoriteDto
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int PropertyId { get; set; }
    public DateTime CreatedDate { get; set; }
}
