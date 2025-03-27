using ComicTracker.Application.Interfaces;
using ComicTracker.Application.Services;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using ComicTracker.Infrastructure.Repositories;
using ComicTracker.Infrastructure.Services.ComicVine;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ComicTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Comic Vine
builder.Services.Configure<ComicVineSettings>(builder.Configuration.GetSection("ComicVine"));
builder.Services.AddHttpClient<IComicVineService, ComicVineService>();

// Registrar repositórios e serviços
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IComicVineService, ComicVineService>();

builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ComicTracker API",
        Version = "v1",
        Description = "API para gerenciamento de coleções de HQs",
        Contact = new OpenApiContact
        {
            Name = "Maurício Oliveira",
            Email = "mauridf@gmail.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicTracker API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();