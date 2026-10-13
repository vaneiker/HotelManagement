
using HotelManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Connection String
var connectionString =
    builder.Configuration.GetConnectionString("HotelManagement")
    ?? throw new InvalidOperationException(
        "Connection string 'HotelManagement' no encontrada.");

// Dependency Injection
builder.Services.AddHotelManagementServices(
    connectionString,
    builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
});

// Redirigir / hacia Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
