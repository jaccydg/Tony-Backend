using Swashbuckle.AspNetCore.Swagger;
using Tony_Backend.Application.Commands.GatewayCommands;
using Tony_Backend.Shared.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Set the port to 8080
builder.WebHost.UseUrls("http://*:5005");

// Add services to the container.
builder.Services.AddScoped<IQueueOperations, QueueOperations>();

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
