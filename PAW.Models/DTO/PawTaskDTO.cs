namespace PAW.Models.DTO;

public class PawTaskDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }

    public static PawTaskDTO ConvertFrom(PawTask entity) => new()
    {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Status = entity.Status,
            DueDate = entity.DueDate,
            CreatedAt = entity.CreatedAt,
            LastModified = entity.LastModified,
            ModifiedBy = entity.ModifiedBy
    };

    public static PawTask ConvertTo(PawTaskDTO dto)
    {
        var entity = new PawTask { Id = dto.Id };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(PawTask entity)
    {
        entity.Name = Name;
        entity.Description = Description;
        entity.Status = Status;
        entity.DueDate = DueDate;
        entity.CreatedAt = CreatedAt;
        entity.LastModified = LastModified;
        entity.ModifiedBy = ModifiedBy;
    }
}
