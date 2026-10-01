namespace PAW.Models.DTO;

public class ProductDTO
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public decimal? Rating { get; set; }
    public int? CategoryId { get; set; }
    public int? InventoryId { get; set; }
    public int? SupplierId { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
    public string? CreatedBy { get; set; }

    public static ProductDTO ConvertFrom(Product entity) => new()
    {
            ProductId = entity.ProductId,
            ProductName = entity.ProductName,
            Description = entity.Description,
            Rating = entity.Rating,
            CategoryId = entity.CategoryId,
            InventoryId = entity.InventoryId,
            SupplierId = entity.SupplierId,
            LastModified = entity.LastModified,
            ModifiedBy = entity.ModifiedBy,
            CreatedBy = entity.CreatedBy
    };

    public static Product ConvertTo(ProductDTO dto)
    {
        var entity = new Product { ProductId = dto.ProductId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Product entity)
    {
        entity.ProductName = ProductName;
        entity.Description = Description;
        entity.Rating = Rating;
        entity.CategoryId = CategoryId;
        entity.InventoryId = InventoryId;
        entity.SupplierId = SupplierId;
        entity.LastModified = LastModified;
        entity.ModifiedBy = ModifiedBy;
        entity.CreatedBy = CreatedBy;
    }
}
