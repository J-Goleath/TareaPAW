using Microsoft.AspNetCore.Mvc;

namespace PAW.Web.Controllers;

/// <summary>
/// Clase base de los controllers MVC de PAW.Web.
/// ANTES: aquí se pedía el tipo de cambio en el servidor (con .Result) y se
/// mandaba a cada vista.
/// AHORA (Tarea 2): el tipo de cambio lo carga JavaScript desde site.js,
/// así que esta clase ya no lo necesita. Se deja como punto común por si
/// luego se quiere agregar lógica compartida a todos los controllers.
/// </summary>
public class ControllerBase : Controller
{
}
