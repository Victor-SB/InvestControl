using InvestControl.Application.DTO;
using InvestControl.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de CORS com uma política nomeada
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7283") // URL do projeto Blazor WebAssembly
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Adiciona o DbContext com Pomelo + MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))
    )
);

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Controllers com opções JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowBlazorClient");

app.UseAuthorization();
app.MapControllers();
app.Run();
