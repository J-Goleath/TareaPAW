namespace PAW.Models.DTO;

public class UserActionDTO
{
    public decimal? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public static UserActionDTO ConvertFrom(UserAction entity) => new()
    {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
    };

    public static UserAction ConvertTo(UserActionDTO dto)
    {
        var entity = new UserAction { Id = dto.Id };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(UserAction entity)
    {
        entity.Name = Name;
        entity.Description = Description;
    }
}
