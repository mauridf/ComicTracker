using ComicTracker.Application.Interfaces;
using ComicTracker.Application.Services;
using ComicTracker.Domain.Interfaces;
using ComicTracker.Infrastructure.Data;
using ComicTracker.Infrastructure.Repositories;
using ComicTracker.Infrastructure.Services.ComicVine;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "Logs/comictracker-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Registrar repositórios e serviços
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddScoped<IVolumeService, VolumeService>();
builder.Services.AddScoped<IVolumeRepository, VolumeRepository>();

builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();

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