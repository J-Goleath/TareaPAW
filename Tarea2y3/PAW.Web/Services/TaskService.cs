using APW.Architecture;
using PAW.Architecture.Providers;
using PAW.Models.DTO;

namespace PAW.Web.Services;

/// <summary>
/// Contrato del servicio de tareas que usa la app web.
/// El TaskController solo conoce esta interfaz (se la inyectan por DI).
/// </summary>
public interface ITaskService
{
    Task<IEnumerable<TaskDTO>> GetTasksAsync();
    Task<TaskDTO?> GetTaskAsync(int id);
    Task<bool> CreateTaskAsync(TaskDTO task);
    Task<bool> UpdateTaskAsync(int id, TaskDTO task);
    Task<bool> PatchStatusAsync(int id, string status);
    Task<bool> DeleteTaskAsync(int id);
}

/// <summary>
/// Implementación: llama por HTTP a PAW.MinimalAPI usando el RestProvider
/// (la clase de PAW.Architecture que ya hace GET/POST/PUT/PATCH/DELETE).
/// La URL de la Minimal API se lee de appsettings.json (MinimalApi:BaseUrl).
/// </summary>
public class TaskService : ServiceBase, ITaskService
{
    private const string _path = "tasks/";
    private readonly IRestProvider _restProvider;

    public TaskService(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        BaseUrl = configuration["MinimalApi:BaseUrl"]
            ?? throw new InvalidOperationException("Falta MinimalApi:BaseUrl en appsettings.json");
    }

    // Ej.: https://localhost:7090/tasks/
    private string Endpoint => SetPathUrl(_path);

    // GET /tasks
    public async Task<IEnumerable<TaskDTO>> GetTasksAsync()
    {
        var response = await _restProvider.GetAsync(Endpoint, id: null);
        return await JsonProvider.DeserializeAsync<List<TaskDTO>>(response) ?? new List<TaskDTO>();
    }

    // GET /tasks/{id}  (si no existe la API responde 404 -> devolvemos null)
    public async Task<TaskDTO?> GetTaskAsync(int id)
    {
        try
        {
            var response = await _restProvider.GetAsync(Endpoint, id.ToString());
            return await JsonProvider.DeserializeAsync<TaskDTO>(response);
        }
        catch (ApplicationException)
        {
            return null;
        }
    }

    // POST /tasks
    public async Task<bool> CreateTaskAsync(TaskDTO task)
        => await TryAsync(() => _restProvider.PostAsync(Endpoint, JsonProvider.Serialize(task)));

    // PUT /tasks/{id}
    public async Task<bool> UpdateTaskAsync(int id, TaskDTO task)
        => await TryAsync(() => _restProvider.PutAsync(Endpoint, id.ToString(), JsonProvider.Serialize(task)));

    // PATCH /tasks/{id}  (solo cambia el estado)
    public async Task<bool> PatchStatusAsync(int id, string status)
        => await TryAsync(() => _restProvider.PatchAsync(Endpoint, id.ToString(),
               JsonProvider.Serialize(new TaskPatchDTO { Status = status })));

    // DELETE /tasks/{id}
    public async Task<bool> DeleteTaskAsync(int id)
        => await TryAsync(() => _restProvider.DeleteAsync(Endpoint, id.ToString()));

    // Si la API responde error (400, 404, 500...), el RestProvider lanza
    // ApplicationException. Aquí lo convertimos en true / false.
    private static async Task<bool> TryAsync(Func<Task<string>> call)
    {
        try
        {
            await call();
            return true;
        }
        catch (ApplicationException)
        {
            return false;
        }
    }
}
