// "TaskEntity" es un apodo (alias) para PAW.Models.Task.
// Se usa porque "Task" choca con System.Threading.Tasks.Task (lo asíncrono).
using TaskEntity = PAW.Models.Task;

namespace PAW.MinimalAPI.Data;

/// <summary>
/// CONTRATO (interfaz) de dónde salen los datos de las tareas.
/// Los endpoints solo conocen esta interfaz, NO saben si los datos vienen
/// de una lista en memoria, de SQL Server o de un archivo.
/// Eso es Inversión de Control (IoC): quien decide la implementación es
/// Program.cs al registrarla en el contenedor de Dependency Injection.
/// </summary>
public interface ITaskRepository
{
    IEnumerable<TaskEntity> GetAll();
    TaskEntity? GetById(int id);
    IEnumerable<TaskEntity> GetByStatus(string status);
    TaskEntity Create(TaskEntity task);
    bool Update(TaskEntity task);
    bool Delete(int id);
}
