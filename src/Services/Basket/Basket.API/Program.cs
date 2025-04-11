var builder = WebApplication.CreateBuilder(args);

// Add Services To The Container
var app = builder.Build();

// Configure The Http Request Pipeline 
app.Run();
