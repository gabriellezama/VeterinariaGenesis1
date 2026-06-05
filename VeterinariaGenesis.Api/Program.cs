using Microsoft.AspNetCore.StaticFiles;
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

// 1) Archivos estáticos PRIMERO (antes del routing)
// Necesario para que _framework/blazor.webassembly.js sea encontrado
app.UseDefaultFiles();

// Registrar MIME types para archivos Blazor WASM
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".wasm"]  = "application/wasm";
provider.Mappings[".blat"]  = "application/octet-stream";
provider.Mappings[".dat"]   = "application/octet-stream";
provider.Mappings[".dll"]   = "application/octet-stream";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// 2) Luego routing y CORS
app.UseRouting();
app.UseCors("AllowAll");

// 3) Diagnóstico de errores
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

// 4) Fallback: cualquier ruta desconocida → index.html (necesario para Blazor routing)
app.MapFallbackToFile("index.html");

app.Run();
