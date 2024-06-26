using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tony_Backend.API.Data;
using Tony_Backend.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});

// Add Auth Provider
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
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

builder.Services.AddControllers();
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

app.MapIdentityApi<IdentityUser>();

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
