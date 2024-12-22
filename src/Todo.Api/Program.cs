using MediatR;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Scalar.AspNetCore;
using Serilog;
using Todo.Api.Authentication;
using Todo.Api.Migrations;
using Todo.Application.Behaviours;
using Todo.Application.Extensions;
using Todo.Infrastructure.Extensions;
using Todo.IntegrationEvents.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .ApplicationLayerExtension(builder.Configuration)
    .InfrastructureLayerExtension(builder.Configuration);
//.IntegrationEventExtension(builder.Configuration);

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

builder.Services
    .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

// Configure JwtBearer authentication
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services
    .AddAuthentication(BearerTokenDefaults.AuthenticationScheme)
    .AddBearerToken();

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
    app.ApplyMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("AllowAny");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();