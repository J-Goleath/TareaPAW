namespace PAW.Models.DTO;

public class ComponentDTO
{
    public decimal Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public static ComponentDTO ConvertFrom(Component entity) => new()
    {
            Id = entity.Id,
            Name = entity.Name,
            Content = entity.Content
    };

    public static Component ConvertTo(ComponentDTO dto)
    {
        var entity = new Component { Id = dto.Id };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Component entity)
    {
        entity.Name = Name;
        entity.Content = Content;
    }
}
