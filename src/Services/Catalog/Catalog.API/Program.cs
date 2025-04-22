
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(opts=>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// Initial Dev Env Data

if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();

// Configure Http request pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
//        if (exception == null)
//        {
//            return;
//        }

//        var problemDetails = new ProblemDetails 
//        { 
//            Title =exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail=exception.StackTrace
//        };


//        var logger= context.RequestServices.GetService<ILogger<Program>>();

//        logger.LogError(exception,exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

//        // using static System.Net.Mime.MediaTypeNames;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);

//    });
//});

app.Run();
