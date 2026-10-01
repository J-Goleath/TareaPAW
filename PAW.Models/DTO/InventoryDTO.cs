namespace PAW.Models.DTO;

public class InventoryDTO
{
    public int InventoryId { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public int? ProductId { get; set; }
    public DateTime? DateAdded { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? ModifiedBy { get; set; }

    public static InventoryDTO ConvertFrom(Inventory entity) => new()
    {
            InventoryId = entity.InventoryId,
            UnitPrice = entity.UnitPrice,
            UnitsInStock = entity.UnitsInStock,
            ProductId = entity.ProductId,
            DateAdded = entity.DateAdded,
            LastUpdated = entity.LastUpdated,
            ModifiedBy = entity.ModifiedBy
    };

    public static Inventory ConvertTo(InventoryDTO dto)
    {
        var entity = new Inventory { InventoryId = dto.InventoryId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Inventory entity)
    {
        entity.UnitPrice = UnitPrice;
        entity.UnitsInStock = UnitsInStock;
        entity.ProductId = ProductId;
        entity.DateAdded = DateAdded;
        entity.LastUpdated = LastUpdated;
        entity.ModifiedBy = ModifiedBy;
    }
}
