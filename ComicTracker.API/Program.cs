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

// Registrar HttpClient e ComicVineService
builder.Services.AddHttpClient<IComicVineService, ComicVineService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ComicVine:BaseUrl"]);
    client.DefaultRequestHeaders.Add("User-Agent", "ComicTracker/1.0");
    client.Timeout = TimeSpan.FromSeconds(30);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
});

// Registrar repositórios e serviços
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ComicTracker API",
        Version = "v1",
        Description = "API para gerenciamento de coleções de HQs"
    });
});

var app = builder.Build();

// Configure o pipeline HTTP (substitua toda a parte após o builder.Build())
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ComicTracker API v1");
        c.RoutePrefix = "swagger"; // Isso faz com que o Swagger seja a página inicial
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();