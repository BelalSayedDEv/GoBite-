namespace GoBite.Application.DTOs.Categories;

public class UpdateCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
