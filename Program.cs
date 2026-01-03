using ExpensesTracker.Data;
using ExpensesTracker.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION SERILOG
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

// Remplacer le logger par défaut
builder.Host.UseSerilog();

builder.Logging.ClearProviders();


// Adding EF core registration 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// Add services to the container and Fluent Validation 

builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
    {
        //fv.RegisterValidatorsFromAssemblyContaining<>();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//adding Identity to the project for the authentification 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// adding Authorisation and Authentification 
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

//  Middleware global d'exception
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
