using NetworkService.Api.Extensions;
using NetworkService.Api.Services;
using NetworkService.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNetworkServiceApplicationServices();
builder.Services.AddTransient<IService, Service>();

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

app.RegisterNetworkServiceEndpoints();
app.Run();

