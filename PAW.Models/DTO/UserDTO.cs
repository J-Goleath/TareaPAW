namespace PAW.Models.DTO;

public class UserDTO
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
    public int? RoleId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
    public string? ModifiedBy { get; set; }
    public string? LastModifiedBy { get; set; }

    public static UserDTO ConvertFrom(User entity) => new()
    {
            UserId = entity.UserId,
            Username = entity.Username,
            Email = entity.Email,
            IsActive = entity.IsActive,
            RoleId = entity.RoleId,
            CreatedAt = entity.CreatedAt,
            LastModified = entity.LastModified,
            ModifiedBy = entity.ModifiedBy,
            LastModifiedBy = entity.LastModifiedBy
    };

    public static User ConvertTo(UserDTO dto)
    {
        var entity = new User { UserId = dto.UserId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(User entity)
    {
        entity.Username = Username;
        entity.Email = Email;
        entity.IsActive = IsActive;
        entity.RoleId = RoleId;
        entity.CreatedAt = CreatedAt;
        entity.LastModified = LastModified;
        entity.ModifiedBy = ModifiedBy;
        entity.LastModifiedBy = LastModifiedBy;
    }
}
