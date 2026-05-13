using VeterinariaGenesis.Application;
using VeterinariaGenesis.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// CORS CONFIGURATION - MODO "PUERTAS ABIERTAS" TOTAL
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// EL ORDEN ES CRÍTICO EN .NET 10
app.UseRouting(); // Primero activamos el mapa de rutas

app.UseCors("AllowAll"); // Luego ponemos el sello de aprobación

app.UseAuthorization();

app.MapControllers();

app.Run();
