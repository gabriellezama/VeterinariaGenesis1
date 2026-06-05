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
app.UseRouting();

app.UseCors("AllowAll");

// Servir archivos estáticos del Cliente Blazor WASM
app.UseDefaultFiles();
app.UseStaticFiles();

// Middleware de diagnóstico para ver errores reales en la consola de Railway
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR CRÍTICO]: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"Error interno: {ex.Message}");
    }
});

app.UseAuthorization();
app.MapControllers();

// Fallback: todas las rutas desconocidas van al index.html del Cliente
app.MapFallbackToFile("index.html");

app.Run();
