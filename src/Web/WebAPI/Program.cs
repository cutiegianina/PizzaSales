using Serilog;
using System.Reflection;
using Presentation;
using WebAPI.Middlewares;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));


// Services are registered here
IServiceCollection services = builder.Services;
IConfiguration config = builder.Configuration;

// Get Presentation Assembly to use its controllers externally in WebAPI to prevent Presentation dependency on Infrastructure
Assembly presentationAssembly = typeof(AssemblyReference).Assembly;
services.AddControllers()
        .AddApplicationPart(presentationAssembly);
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAuthentication();
services.AddAuthorization();
services.AddApplicationServices();
services.AddInfrastructureServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();