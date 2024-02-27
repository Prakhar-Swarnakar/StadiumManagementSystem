using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StadiumTrafficManager.Repository.Interface;
using StadiumTrafficManager.Repository.Persistance.Context;
using StadiumTrafficManager.Repository.Persistance.Repository;
using StadiumTrafficManager.SensorDataService;
using StadiumTrafficManager.Service.Implementation;
using StadiumTrafficManager.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
//builder.Services.AddDbContext<StadiumManagerContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContextFactory<StadiumManagerContext>(opts => opts.UseSqlServer(connectionString));
builder.Services.AddDbContextFactory<StadiumManagerContext>(options => options.UseNpgsql(connectionString));

//uilder.Services.AddDbContextFactory<StadiumManagerContext>(options => options.UseSqlServer(connectionString));
//Add interfaces

builder.Services.AddTransient<IStadiumManagerRepository>(p =>
    new StadiumManagerRepository(
        p.GetRequiredService<IDbContextFactory<StadiumManagerContext>>()));

builder.Services.AddTransient<IStadiumManagerService, StadiumManagerService>();

//builder.Services.AddScoped<IStadiumManagerContextFactory, StadiumManagerContextFactory>();


//Add  hosted service
builder.Services.AddHostedService<SensorDataServiceWorker>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
