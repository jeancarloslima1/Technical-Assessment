using System.Text.Json;
using TechAssess.ScrapingService.Converters;
using TechAssess.ScrapingService.Factories;
using TechAssess.ScrapingService.Scrapers;
using TechAssess.ScrapingService.Services;
using TechAssess.WebScrapingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new ScrapeDataConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
}); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<OLDScraper>();
builder.Services.AddTransient<WBScraper>();
builder.Services.AddTransient<OFACScraper>();
builder.Services.AddSingleton<IScraperFactory, ScraperFactory>();
builder.Services.AddScoped<IScrapingService, ScrapingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
