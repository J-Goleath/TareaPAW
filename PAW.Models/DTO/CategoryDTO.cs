namespace PAW.Models.DTO;

public class CategoryDTO
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }

    public static CategoryDTO ConvertFrom(Category entity) => new()
    {
            CategoryId = entity.CategoryId,
            CategoryName = entity.CategoryName,
            Description = entity.Description,
            LastModified = entity.LastModified,
            ModifiedBy = entity.ModifiedBy
    };

    public static Category ConvertTo(CategoryDTO dto)
    {
        var entity = new Category { CategoryId = dto.CategoryId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Category entity)
    {
        entity.CategoryName = CategoryName;
        entity.Description = Description;
        entity.LastModified = LastModified;
        entity.ModifiedBy = ModifiedBy;
    }
}
