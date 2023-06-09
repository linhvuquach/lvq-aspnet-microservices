using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.SyncDataServices.Grpc;
using PlatformService.SyncDataServices.Http;

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

// SERVICES
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

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

app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
});

PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();


