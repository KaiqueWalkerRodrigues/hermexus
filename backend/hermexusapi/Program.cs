using hermexusapi.Auth.Contract;
using hermexusapi.Auth.Contract.Tools;
using hermexusapi.Configuration;
using hermexusapi.Configurations;
using hermexusapi.Hypermedia.Filters;
using hermexusapi.Repositories;
using hermexusapi.Repositories.Impl;
using hermexusapi.Services;
using hermexusapi.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add Configure files
builder.ConfigureSerilog();
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddHATEOASConfiguration();

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add<HypermediaFilter>();
});

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthConfiguration(builder.Configuration);

builder.Services.AddOpenApi();

// Auth Builders
builder.Services.AddScoped<IPasswordHasher, Sha256PasswordHasher>();
builder.Services.AddScoped<IUserAuthService, UserAuthServiceImpl>();
builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

// Services
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

var app = builder.Build();

app.MapOpenApi();
app.AddScalarConfiguration();

EvolveConfig.ExecuteMigrations(builder.Configuration, builder.Environment);

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCorsConfiguration(builder.Configuration);

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");
//app.Run();
