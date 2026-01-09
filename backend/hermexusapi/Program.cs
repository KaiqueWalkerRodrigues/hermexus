var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if(!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");
//app.Run();
