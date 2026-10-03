namespace Crud;

// Almacén en memoria: los datos se pierden al reiniciar la app.
public class EstudianteRepositorio
{
    private readonly List<Estudiante> _estudiantes = new()
    {
        new Estudiante { Id = 1, Nombre = "Ana", Apellido = "García", Edad = 20, Carrera = "Ingeniería de Sistemas" },
        new Estudiante { Id = 2, Nombre = "Luis", Apellido = "Pérez", Edad = 22, Carrera = "Medicina" },
    };
    private int _siguienteId = 3;

    public IEnumerable<Estudiante> ObtenerTodos() => _estudiantes;

    public Estudiante? ObtenerPorId(int id) => _estudiantes.FirstOrDefault(e => e.Id == id);

    public Estudiante Crear(Estudiante estudiante)
    {
        estudiante.Id = _siguienteId++;
        _estudiantes.Add(estudiante);
        return estudiante;
    }

    public bool Actualizar(int id, Estudiante datos)
    {
        var existente = ObtenerPorId(id);
        if (existente is null) return false;
        existente.Nombre = datos.Nombre;
        existente.Apellido = datos.Apellido;
        existente.Edad = datos.Edad;
        existente.Carrera = datos.Carrera;
        return true;
    }

    public bool Eliminar(int id)
    {
        var existente = ObtenerPorId(id);
        if (existente is null) return false;
        _estudiantes.Remove(existente);
        return true;
    }
}
