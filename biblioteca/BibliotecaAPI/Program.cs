using BibliotecaAPI.Data;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework + MySQL
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("BibliotecaConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("BibliotecaConnection")
        )
    )
);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
