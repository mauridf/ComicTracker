using ComicTracker.Application.Interfaces;
using ComicTracker.Application.Services;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using ComicTracker.Infrastructure.Repositories;
using ComicTracker.Infrastructure.Services.ComicVine;
using Microsoft.EntityFrameworkCore;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();