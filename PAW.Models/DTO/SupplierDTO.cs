namespace PAW.Models.DTO;

public class SupplierDTO
{
    public int SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? ContactName { get; set; }
    public string? ContactTitle { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }

    public static SupplierDTO ConvertFrom(Supplier entity) => new()
    {
            SupplierId = entity.SupplierId,
            SupplierName = entity.SupplierName,
            ContactName = entity.ContactName,
            ContactTitle = entity.ContactTitle,
            Phone = entity.Phone,
            Address = entity.Address,
            City = entity.City,
            Country = entity.Country,
            LastModified = entity.LastModified,
            ModifiedBy = entity.ModifiedBy
    };

    public static Supplier ConvertTo(SupplierDTO dto)
    {
        var entity = new Supplier { SupplierId = dto.SupplierId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Supplier entity)
    {
        entity.SupplierName = SupplierName;
        entity.ContactName = ContactName;
        entity.ContactTitle = ContactTitle;
        entity.Phone = Phone;
        entity.Address = Address;
        entity.City = City;
        entity.Country = Country;
        entity.LastModified = LastModified;
        entity.ModifiedBy = ModifiedBy;
    }
}
