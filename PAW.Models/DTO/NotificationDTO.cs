namespace PAW.Models.DTO;

public class NotificationDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool? IsRead { get; set; }
    public DateTime? CreatedAt { get; set; }

    public static NotificationDTO ConvertFrom(Notification entity) => new()
    {
            Id = entity.Id,
            UserId = entity.UserId,
            Message = entity.Message,
            IsRead = entity.IsRead,
            CreatedAt = entity.CreatedAt
    };

    public static Notification ConvertTo(NotificationDTO dto)
    {
        var entity = new Notification { Id = dto.Id };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Notification entity)
    {
        entity.UserId = UserId;
        entity.Message = Message;
        entity.IsRead = IsRead;
        entity.CreatedAt = CreatedAt;
    }
}
