namespace PAW.Models.DTO;

public class UserRoleDTO
{
    public decimal? Id { get; set; }
    public decimal? RoldId { get; set; }
    public decimal? UserId { get; set; }

    public static UserRoleDTO ConvertFrom(UserRole entity) => new()
    {
            Id = entity.Id,
            RoldId = entity.RoldId,
            UserId = entity.UserId
    };

    public static UserRole ConvertTo(UserRoleDTO dto)
    {
        var entity = new UserRole { Id = dto.Id };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(UserRole entity)
    {
        entity.RoldId = RoldId;
        entity.UserId = UserId;
    }
}
