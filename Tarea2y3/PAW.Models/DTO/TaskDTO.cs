using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PAW.Models.DTO;

/// <summary>
/// DTO (Data Transfer Object) de una tarea.
/// Es la "forma" en que una tarea viaja en JSON entre PAW.MinimalAPI y PAW.Web.
/// Se crea a partir de la entidad PAW.Models.Task (Entities/Task.cs).
///
/// Nota: dentro de este namespace, "Task" significa PAW.Models.Task (la tarea),
/// NO System.Threading.Tasks.Task (lo asíncrono).
/// </summary>
public class TaskDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    [Display(Name = "Nombre")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
    [Display(Name = "Descripción")]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [Display(Name = "Estado")]
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha límite")]
    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }

    [Display(Name = "Creada")]
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Última modificación")]
    [JsonPropertyName("lastModified")]
    public DateTime? LastModified { get; set; }

    [Display(Name = "Modificada por")]
    [JsonPropertyName("modifiedBy")]
    public string? ModifiedBy { get; set; }

    /// <summary>Estados válidos que usa la app.</summary>
    public static readonly string[] Statuses = { "Pendiente", "En progreso", "Completada" };

    /// <summary>Entidad -> DTO</summary>
    public static TaskDTO ConvertFrom(Task task) => new()
    {
        Id = task.Id,
        Name = task.Name,
        Description = task.Description,
        Status = task.Status,
        DueDate = task.DueDate,
        CreatedAt = task.CreatedAt,
        LastModified = task.LastModified,
        ModifiedBy = task.ModifiedBy
    };

    /// <summary>DTO -> Entidad</summary>
    public Task ToEntity() => new()
    {
        Id = Id,
        Name = Name,
        Description = Description,
        Status = Status,
        DueDate = DueDate,
        CreatedAt = CreatedAt,
        LastModified = LastModified,
        ModifiedBy = ModifiedBy
    };
}

/// <summary>
/// DTO para PATCH: actualización PARCIAL.
/// Solo se cambian los campos que vengan con valor (los null se ignoran).
/// </summary>
public class TaskPatchDTO
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("modifiedBy")]
    public string? ModifiedBy { get; set; }
}
