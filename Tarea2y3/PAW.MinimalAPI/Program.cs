using PAW.MinimalAPI.Data;
using PAW.MinimalAPI.Endpoints;

// =====================================================================
//  PAW.MinimalAPI - Program.cs
//  En una Minimal API NO hay controllers: las rutas se declaran aquí
//  (o en un método de extensión como MapTaskEndpoints).
// =====================================================================

var builder = WebApplication.CreateBuilder(args);

// ---------- 1. Registrar servicios en el contenedor de DI ----------

// Dependency Injection / IoC:
// "Cuando alguien pida un ITaskRepository, entregale un InMemoryTaskRepository".
// Singleton = una sola instancia para toda la vida de la API
// (así la lista en memoria no se borra entre peticiones).
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();

// Swagger para probar los endpoints desde el navegador.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- 2. Configurar el pipeline HTTP ----------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---------- 3. Endpoints ----------

app.MapGet("/", () => Results.Redirect("/swagger"))
   .ExcludeFromDescription();

app.MapTaskEndpoints();   // todas las rutas /tasks (ver Endpoints/TaskEndpoints.cs)

app.Run();
