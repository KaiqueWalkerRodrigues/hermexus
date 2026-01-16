using hermexusapi.Auth.Contract;
using hermexusapi.Auth.Contract.Tools;
using hermexusapi.Configurations;
using hermexusapi.Hypermedia.Filters;
using hermexusapi.Repositories;
using hermexusapi.Repositories.Impl;
using hermexusapi.Services;
using hermexusapi.Services.Impl;
using System.Text.Json;

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
builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

// Services
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IRoleService, RoleServiceImpl>();
builder.Services.AddScoped<ISectorService, SectorServiceImpl>();
builder.Services.AddScoped<ICompanyService, CompanyServiceImpl>();
builder.Services.AddScoped<IPermissionService, PermissionServiceImpl>();
builder.Services.AddScoped<IWhatsappContactService, WhatsappContactServiceImpl>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IWhatsappContactRepository, WhatsappContactRepository>();

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
