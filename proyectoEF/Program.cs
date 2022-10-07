using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoEF;
using proyectoEF.Models;
using System.Threading;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

//Agregando conexion a base de datos en memoria
//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));

//Configuracion para base de datos postgreSQL
///Nota: Recordar que la conexion se hace desde el archivo appsettings.json
builder.Services.AddNpgsql<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

//Creando endpoint
//Este endpoint nos notifica si se ha creado la base de datos
app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    //Aqui nos devuelve un true o false para saber si la base de datos esta en memoria o no
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

//Endpoint de tipo GET para filtrado de tareas
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    //Retorna todas las tareas creadas
    return Results.Ok(dbContext.tareas);

    //Filtrando por prioridad de tareas en baja
    ///return Results.Ok(dbContext.tareas.Where(p => p.PrioridadTarea == proyectoEF.Models.Prioridad.Baja));
    
    //Filtrando por prioridad de tareas en baja e incluyendo la categoria de la tarea
    ///return Results.Ok(dbContext.tareas.Include(p => p.Categoria).Where(p => p.PrioridadTarea == proyectoEF.Models.Prioridad.Baja));
});

//Endpoint de tipo POST para ingresar datos en la tabla tareas
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    //Creando tarea
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContext.AddAsync(tarea);

    //Guardando la tarea
    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

//Endpoint de tipo PUT para actualizar tareas (Recordar siempre recoger el id del elemento al que se va actualizar)
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id ) =>
{
    //Buscando la tarea actual por id
    var tareaActual = dbContext.tareas.Find(id);

    //Validando si la tarea fue encontrada
    if(tareaActual != null)
    {
        //Modificando los datos de la tarea
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    } else
    {
        return Results.NotFound();
    }
});

//Endpoint de tipo DELETE para eliminar tareas (Recordar siempre recoger el id del elemento al que se va eliminar)
app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    //Buscando la tarea actual por id
    var tareaActual = dbContext.tareas.Find(id);

    if (tareaActual != null)
    {
        //Eliminando tarea
        dbContext.Remove(tareaActual);
        

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});

//Nota: Recordar probar el endpoint en postman

app.Run();
