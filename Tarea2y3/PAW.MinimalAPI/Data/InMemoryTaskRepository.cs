using TaskEntity = PAW.Models.Task;

namespace PAW.MinimalAPI.Data;

/// <summary>
/// IMPLEMENTACIÓN del repositorio que guarda las tareas en una LISTA EN MEMORIA.
/// - NO usa Entity Framework ni DbContext (lo pide la tarea).
/// - Se registra como Singleton en Program.cs: existe UNA sola instancia
///   mientras la API esté corriendo, por eso los datos se mantienen entre
///   peticiones. Si se detiene la API, se vuelve a los datos de ejemplo.
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskEntity> _tasks;
    private int _nextId;

    // "lock" evita problemas si llegan dos peticiones al mismo tiempo.
    private readonly object _lock = new();

    public InMemoryTaskRepository()
    {
        var now = DateTime.Today;   // hoy a medianoche

        // Datos de ejemplo para que la pantalla no arranque vacía.
        _tasks = new List<TaskEntity>
        {
            new() { Id = 1, Name = "Configurar PAW.MinimalAPI", Description = "Crear el proyecto Web API y los endpoints", Status = "Completada", DueDate = now.AddDays(-2), CreatedAt = now.AddDays(-5), LastModified = now.AddDays(-2), ModifiedBy = "Engel" },
            new() { Id = 2, Name = "Tipo de cambio con JavaScript", Description = "Cargar el tipo de cambio desde site.js con caché", Status = "En progreso", DueDate = now.AddDays(1), CreatedAt = now.AddDays(-3), LastModified = now.AddDays(-1), ModifiedBy = "Engel" },
            new() { Id = 3, Name = "Probar el CRUD en Swagger", Description = "GET, POST, PUT, PATCH y DELETE", Status = "Pendiente", DueDate = now.AddDays(3), CreatedAt = now.AddDays(-1), LastModified = now.AddDays(-1), ModifiedBy = "Engel" },
            new() { Id = 4, Name = "Preparar la defensa", Description = "Explicar DI/IoC y Minimal API", Status = "Pendiente", DueDate = now.AddDays(6), CreatedAt = now, LastModified = now, ModifiedBy = "Engel" }
        };
        _nextId = _tasks.Max(t => t.Id) + 1;
    }

    public IEnumerable<TaskEntity> GetAll()
    {
        lock (_lock) return _tasks.OrderBy(t => t.Id).ToList();
    }

    public TaskEntity? GetById(int id)
    {
        lock (_lock) return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<TaskEntity> GetByStatus(string status)
    {
        lock (_lock)
            return _tasks.Where(t => string.Equals(t.Status, status, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public TaskEntity Create(TaskEntity task)
    {
        lock (_lock)
        {
            task.Id = _nextId++;          // la API asigna el Id, no el cliente
            task.CreatedAt = DateTime.Now;
            task.LastModified = task.CreatedAt;
            _tasks.Add(task);
            return task;
        }
    }

    public bool Update(TaskEntity task)
    {
        lock (_lock)
        {
            var index = _tasks.FindIndex(t => t.Id == task.Id);
            if (index < 0) return false;

            task.CreatedAt = _tasks[index].CreatedAt;   // la fecha de creación no cambia
            task.LastModified = DateTime.Now;
            _tasks[index] = task;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock) return _tasks.RemoveAll(t => t.Id == id) > 0;
    }
}
