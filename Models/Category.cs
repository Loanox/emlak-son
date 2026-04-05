namespace emlak_son.Models;

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool Status { get; set; } = true;

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
