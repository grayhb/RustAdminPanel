using Microsoft.EntityFrameworkCore;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.DAL.Context;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.PlayerConnections;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<RustAdminPanelContext>(options => options.UseSqlite(dbConnectionString));
builder.Services.AddScoped<IEntityRepository<PlayerConnectionLog>, EntityRepository<PlayerConnectionLog>>();

// Services
builder.Services.AddScoped<IPlayerConnectionsService, PlayerConnectionsService>();

// API-KEY
builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
