using Microsoft.AspNetCore.Http.HttpResults;
using PAW.MinimalAPI.Data;
using PAW.Models.DTO;

namespace PAW.MinimalAPI.Endpoints;

/// <summary>
/// Aquí se definen TODAS las rutas de /tasks (como en el tutorial de
/// Microsoft con /todoitems, pero usando el modelo Task).
///
/// En cada método, el parámetro "ITaskRepository repo" NO lo creamos
/// nosotros: ASP.NET Core lo INYECTA automáticamente porque está
/// registrado en Program.cs. Eso es Dependency Injection.
/// </summary>
public static class TaskEndpoints
{
    public static RouteGroupBuilder MapTaskEndpoints(this IEndpointRouteBuilder app)
    {
        // MapGroup: todas las rutas de abajo empiezan con /tasks
        var tasks = app.MapGroup("/tasks").WithTags("Tasks");

        tasks.MapGet("/", GetAllTasks);                     // GET    /tasks
        tasks.MapGet("/{id:int}", GetTask);                 // GET    /tasks/5
        tasks.MapGet("/status/{status}", GetTasksByStatus); // GET    /tasks/status/Pendiente
        tasks.MapPost("/", CreateTask);                     // POST   /tasks
        tasks.MapPut("/{id:int}", UpdateTask);              // PUT    /tasks/5
        tasks.MapPatch("/{id:int}", PatchTask);             // PATCH  /tasks/5
        tasks.MapDelete("/{id:int}", DeleteTask);           // DELETE /tasks/5

        return tasks;
    }

    // GET /tasks -> lista todas las tareas
    static Ok<TaskDTO[]> GetAllTasks(ITaskRepository repo)
    {
        var items = repo.GetAll().Select(TaskDTO.ConvertFrom).ToArray();
        return TypedResults.Ok(items);
    }

    // GET /tasks/{id} -> una tarea, o 404 si no existe
    static Results<Ok<TaskDTO>, NotFound> GetTask(int id, ITaskRepository repo)
    {
        var task = repo.GetById(id);
        return task is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(TaskDTO.ConvertFrom(task));
    }

    // GET /tasks/status/{status} -> filtra por estado
    static Ok<TaskDTO[]> GetTasksByStatus(string status, ITaskRepository repo)
    {
        var items = repo.GetByStatus(status).Select(TaskDTO.ConvertFrom).ToArray();
        return TypedResults.Ok(items);
    }

    // POST /tasks -> crea una tarea nueva y devuelve 201 Created
    static Results<Created<TaskDTO>, ValidationProblem> CreateTask(TaskDTO dto, ITaskRepository repo)
    {
        var errors = Validate(dto);
        if (errors.Count > 0) return TypedResults.ValidationProblem(errors);

        var created = repo.Create(dto.ToEntity());
        var result = TaskDTO.ConvertFrom(created);
        return TypedResults.Created($"/tasks/{result.Id}", result);
    }

    // PUT /tasks/{id} -> reemplaza la tarea COMPLETA
    static Results<NoContent, NotFound, ValidationProblem> UpdateTask(int id, TaskDTO dto, ITaskRepository repo)
    {
        var errors = Validate(dto);
        if (errors.Count > 0) return TypedResults.ValidationProblem(errors);

        var entity = dto.ToEntity();
        entity.Id = id;   // el id de la URL manda

        return repo.Update(entity)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }

    // PATCH /tasks/{id} -> cambia SOLO los campos que vengan (ej. solo el estado)
    static Results<Ok<TaskDTO>, NotFound, ValidationProblem> PatchTask(int id, TaskPatchDTO patch, ITaskRepository repo)
    {
        var task = repo.GetById(id);
        if (task is null) return TypedResults.NotFound();

        if (patch.Status is not null && !TaskDTO.Statuses.Contains(patch.Status))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                ["status"] = new[] { "Estado inválido. Use: " + string.Join(", ", TaskDTO.Statuses) }
            });
        }

        // Copiamos la tarea y cambiamos solo lo que vino en el PATCH.
        var updated = TaskDTO.ConvertFrom(task).ToEntity();
        if (patch.Name is not null) updated.Name = patch.Name;
        if (patch.Description is not null) updated.Description = patch.Description;
        if (patch.Status is not null) updated.Status = patch.Status;
        if (patch.DueDate is not null) updated.DueDate = patch.DueDate;
        if (patch.ModifiedBy is not null) updated.ModifiedBy = patch.ModifiedBy;

        repo.Update(updated);
        return TypedResults.Ok(TaskDTO.ConvertFrom(repo.GetById(id)!));
    }

    // DELETE /tasks/{id} -> borra la tarea
    static Results<NoContent, NotFound> DeleteTask(int id, ITaskRepository repo)
    {
        return repo.Delete(id)
            ? TypedResults.NoContent()
            : TypedResults.NotFound();
    }

    // Validaciones básicas antes de guardar.
    static Dictionary<string, string[]> Validate(TaskDTO dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors["name"] = new[] { "El nombre es obligatorio." };

        if (string.IsNullOrWhiteSpace(dto.Status) || !TaskDTO.Statuses.Contains(dto.Status))
            errors["status"] = new[] { "Estado inválido. Use: " + string.Join(", ", TaskDTO.Statuses) };

        return errors;
    }
}
