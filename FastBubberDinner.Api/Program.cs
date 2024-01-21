using FastBubberDinner;
using FastBubberDinner.Api.Middleware;
using FastBubberDinner.Application;
using FastBubberDinner.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();