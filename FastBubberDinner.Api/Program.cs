using FastBubberDinner.Application;
using FastBubberDinner.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();

app.UseFastEndpoints().UseSwaggerGen();
app.Run();