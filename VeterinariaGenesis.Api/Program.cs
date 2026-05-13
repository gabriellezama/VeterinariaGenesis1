using VeterinariaGenesis.Application;
using VeterinariaGenesis.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Clean Architecture Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Configure CORS - MODO ULTRA PERMISIVO
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true) // Acepta CUALQUIER origen (GitHub, Local, etc)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// EL ORDEN ES CRÍTICO: CORS DEBE IR PRIMERO
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
