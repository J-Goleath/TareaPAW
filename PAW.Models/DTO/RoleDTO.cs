namespace PAW.Models.DTO;

public class RoleDTO
{
    public int RoleId { get; set; }
    public string? RoleName { get; set; }

    public static RoleDTO ConvertFrom(Role entity) => new()
    {
            RoleId = entity.RoleId,
            RoleName = entity.RoleName
    };

    public static Role ConvertTo(RoleDTO dto)
    {
        var entity = new Role { RoleId = dto.RoleId };
        dto.ApplyTo(entity);
        return entity;
    }

    public void ApplyTo(Role entity)
    {
        entity.RoleName = RoleName;
    }
}
