# CRUD sencillo en .NET

Minimal API en ASP.NET Core con un CRUD de estudiantes almacenados en memoria.

## Ejecutar

```
dotnet run
```

La API queda en `http://localhost:5118`.

## Endpoints

| Método | Ruta                | Descripción         |
|--------|---------------------|---------------------|
| GET    | /estudiantes        | Lista todos         |
| GET    | /estudiantes/{id}   | Obtiene uno         |
| POST   | /estudiantes        | Crea uno            |
| PUT    | /estudiantes/{id}   | Actualiza uno       |
| DELETE | /estudiantes/{id}   | Elimina uno         |

Cuerpo JSON para POST y PUT:

```json
{ "nombre": "María", "apellido": "López", "edad": 19, "carrera": "Derecho" }
```

El archivo `Crud.http` tiene peticiones de ejemplo listas para usar desde VS Code (extensión REST Client) o Visual Studio.
