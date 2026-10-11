using Microsoft.AspNetCore.Mvc;
using PAW.Models.DTO;
using PAW.Web.Services;

namespace PAW.Web.Controllers;

/// <summary>
/// Controller MVC de la pantalla "Tasks".
/// NO toca datos directamente: todo lo pide al ITaskService, que a su vez
/// llama a PAW.MinimalAPI. El servicio llega por el constructor (DI).
/// </summary>
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET /Task  -> lista
    public async Task<IActionResult> Index()
    {
        try
        {
            var tasks = await _taskService.GetTasksAsync();
            return View(tasks);
        }
        catch (ApplicationException)
        {
            // Pasa si PAW.MinimalAPI no está corriendo.
            TempData["Error"] = "No se pudo conectar con PAW.MinimalAPI. ¿Está corriendo?";
            return View(Enumerable.Empty<TaskDTO>());
        }
    }

    // GET /Task/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var task = await _taskService.GetTaskAsync(id);
        return task is null ? NotFound() : View(task);
    }

    // GET /Task/Create  -> muestra el formulario vacío
    public IActionResult Create()
    {
        return View(new TaskDTO { Status = "Pendiente", DueDate = DateTime.Today.AddDays(7) });
    }

    // POST /Task/Create  -> recibe el formulario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskDTO task)
    {
        if (!ModelState.IsValid) return View(task);

        if (await _taskService.CreateTaskAsync(task))
        {
            TempData["Success"] = "Tarea creada.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "La API no pudo crear la tarea.");
        return View(task);
    }

    // GET /Task/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetTaskAsync(id);
        return task is null ? NotFound() : View(task);
    }

    // POST /Task/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskDTO task)
    {
        if (id != task.Id) return BadRequest();
        if (!ModelState.IsValid) return View(task);

        if (await _taskService.UpdateTaskAsync(id, task))
        {
            TempData["Success"] = "Tarea actualizada.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "La API no pudo actualizar la tarea.");
        return View(task);
    }

    // POST /Task/Complete/5  -> usa PATCH para cambiar solo el estado
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Complete(int id)
    {
        if (await _taskService.PatchStatusAsync(id, "Completada"))
            TempData["Success"] = $"Tarea #{id} marcada como completada.";
        else
            TempData["Error"] = $"No se pudo completar la tarea #{id}.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Task/Delete/5  -> pantalla de confirmación
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskService.GetTaskAsync(id);
        return task is null ? NotFound() : View(task);
    }

    // POST /Task/Delete/5  -> borra de verdad
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (await _taskService.DeleteTaskAsync(id))
            TempData["Success"] = $"Tarea #{id} eliminada.";
        else
            TempData["Error"] = $"No se pudo eliminar la tarea #{id}.";
        return RedirectToAction(nameof(Index));
    }
}
