using hermexusapi.Configuration;
using hermexusapi.Configurations;
using RestWithASPNET10Erudio.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add Configure files
builder.ConfigureSerilog();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddEvolveConfiguration(builder.Configuration, builder.Environment);


builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.AddScalarConfiguration();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");
//app.Run();
