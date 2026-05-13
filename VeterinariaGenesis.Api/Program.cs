using VeterinariaGenesis.Application;
using VeterinariaGenesis.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// CORS CONFIGURATION - MÁXIMA PRIORIDAD
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); // Ayuda con las peticiones de prueba (OPTIONS)
    });
});

var app = builder.Build();

// EL CORS DEBE SER LO PRIMERITO QUE SE EJECUTA
app.UseCors("AllowAll");

// Quitamos HttpsRedirection temporalmente para evitar bloqueos por redirección
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

app.Run();
