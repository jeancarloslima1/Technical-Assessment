using Microsoft.EntityFrameworkCore;
using TechAssess.SupplierService.Data;
using TechAssess.SupplierService.Exceptions;
using TechAssess.SupplierService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddMemoryCache();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<ValidationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
