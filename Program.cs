using Crud;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<EstudianteRepositorio>();

// Swagger: genera la documentación OpenAPI y la interfaz web
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new() { Title = "API CRUD de Estudiantes", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

var estudiantes = app.MapGroup("/estudiantes").WithTags("Estudiantes");

// READ (todos)
estudiantes.MapGet("/", (EstudianteRepositorio repo) => repo.ObtenerTodos())
    .WithSummary("Lista todos los estudiantes");

// READ (uno)
estudiantes.MapGet("/{id:int}", (int id, EstudianteRepositorio repo) =>
    repo.ObtenerPorId(id) is Estudiante e ? Results.Ok(e) : Results.NotFound())
    .WithSummary("Obtiene un estudiante por id")
    .Produces<Estudiante>(200)
    .Produces(404);

// CREATE
estudiantes.MapPost("/", (Estudiante estudiante, EstudianteRepositorio repo) =>
{
    if (string.IsNullOrWhiteSpace(estudiante.Nombre) || string.IsNullOrWhiteSpace(estudiante.Apellido))
        return Results.BadRequest("El nombre y el apellido son obligatorios.");
    var creado = repo.Crear(estudiante);
    return Results.Created($"/estudiantes/{creado.Id}", creado);
})
    .WithSummary("Crea un estudiante")
    .Produces<Estudiante>(201)
    .Produces(400);

// UPDATE
estudiantes.MapPut("/{id:int}", (int id, Estudiante estudiante, EstudianteRepositorio repo) =>
    repo.Actualizar(id, estudiante) ? Results.NoContent() : Results.NotFound())
    .WithSummary("Actualiza un estudiante")
    .Produces(204)
    .Produces(404);

// DELETE
estudiantes.MapDelete("/{id:int}", (int id, EstudianteRepositorio repo) =>
    repo.Eliminar(id) ? Results.NoContent() : Results.NotFound())
    .WithSummary("Elimina un estudiante")
    .Produces(204)
    .Produces(404);

app.Run();
