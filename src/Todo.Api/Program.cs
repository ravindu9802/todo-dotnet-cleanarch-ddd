using MediatR;
using Scalar.AspNetCore;
using Serilog;
using Todo.Application.Behaviours;
using Todo.Application.Extensions;
using Todo.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .ApplicationLayerExtension(builder.Configuration)
    .InfrastructureLayerExtension(builder.Configuration);

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

builder.Services
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Todo API");
        options.WithTheme(ScalarTheme.BluePlanet);
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();