using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database
if (builder.Environment.IsProduction())
{
    Console.WriteLine("Using sql sever DB");
    builder.Services.AddDbContext<AppDbContext>(opt =>
                         opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    // Console.WriteLine("Using in memory database");
    // builder.Services.AddDbContext<AppDbContext>(opt =>
    //                      opt.UseInMemoryDatabase("InMem"));

    Console.WriteLine("Using sql sever DB Local");
    builder.Services.AddDbContext<AppDbContext>(opt =>
                         opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
});
//builder.Services.AddSwaggerGen();

Console.WriteLine($"CommandService endpoint: {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));

}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();


