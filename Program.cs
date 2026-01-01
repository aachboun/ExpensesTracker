using ExpensesTracker.Data;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Adding EF core registration 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// Add services to the container.

builder.Services.AddControllers();
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
