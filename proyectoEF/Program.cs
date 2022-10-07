using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoEF;

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

//Nota: Recordar probar el endpoint en postman


app.Run();
