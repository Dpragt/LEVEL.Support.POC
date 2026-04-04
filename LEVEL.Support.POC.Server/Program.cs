using LEVEL.Support.POC.Server.Agents;
using LEVEL.Support.POC.Server.Apis;
using LEVEL.Support.POC.Server.Data;
using LEVEL.Support.POC.Server.Orchestration;
using LEVEL.Support.POC.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseInMemoryDatabase("MeldingenDb"));

// AI Chat Client configuratie
var openAiKey = builder.Configuration["OpenAI:ApiKey"];
var openAiModel = builder.Configuration["OpenAI:Model"] ?? "gpt-4o-mini";

if (!string.IsNullOrEmpty(openAiKey))
{
    builder.Services.AddChatClient(new OpenAIClient(openAiKey).GetChatClient(openAiModel).AsIChatClient());
}
else
{
    builder.Services.AddSingleton<IChatClient>(sp =>
        throw new InvalidOperationException(
            "OpenAI API key niet geconfigureerd. Stel 'OpenAI:ApiKey' in via appsettings of user secrets."));
}

builder.Services.AddScoped<ClassificationAgent>();
builder.Services.AddScoped<DuplicateDetectionAgent>();
builder.Services.AddScoped<RetrievalService>();
builder.Services.AddScoped<MeldingOrchestrator>();

var app = builder.Build();

// Seed the in-memory database.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    DataSeeder.Seed(db);
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var api = app.MapGroup("/api");

api.MapGroup("/meldingen")
   .MapMeldingen()
   .MapOplossingen()
   .MapGekoppeldeMeldingen();

app.MapDefaultEndpoints();

app.UseFileServer();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
