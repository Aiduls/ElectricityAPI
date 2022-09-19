using ElectricityAPI1;
using ElectricityAPI1.Data;
using ElectricityAPI1.Interfaces;
using ElectricityAPI1.Repository;
using Microsoft.EntityFrameworkCore;
using ElectricityAPI1.Controllers;
using ElectricityAPI1.Models;

var builder = WebApplication.CreateBuilder(args);

LogSystem.init();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<IElectricityRepository, ElectricityRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});  

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

LogSystem.log("Web API has started.");
app.Run();

