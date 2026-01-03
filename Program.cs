using ExpensesTracker.Data;
using ExpensesTracker.Models;
using ExpensesTracker.Services;
using ExpensesTracker.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
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


//configure swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//configuring jwt for Authentification   
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});



//adding Identity to the project for the authentification 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// adding Authorisation and Authentification 
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

//Services Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();


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
