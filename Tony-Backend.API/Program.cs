using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Tony_Backend.API.Data;
using Tony_Backend.Application;
using Tony_Backend.Shared.Entities;
using Tony_Backend.Shared.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.api.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});

builder.Services.AddScoped<IQueueOperations, QueueOperations>();

// Add Auth Provider
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Enable Cors
builder.Services.AddCors(o => o.AddPolicy("AllowAllPolicy", builder =>
{
    builder.WithOrigins(
        "https://localhost:8080",
        "https://192.168.19.225:8080"
        )
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(Discovery).Assembly
    );
});

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseCors("AllowAllPolicy");

app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
