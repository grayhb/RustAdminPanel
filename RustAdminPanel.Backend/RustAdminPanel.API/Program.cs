using Microsoft.EntityFrameworkCore;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.DAL.Context;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.ChatMessages;
using RustAdminPanel.Services.PlayerConnections;
using RustAdminPanel.Services.PlayerReports;
using RustAdminPanel.Services.Profiles;
using RustAdminPanel.Services.Steam;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var corsUrls = builder.Configuration["CorsUrls"] ?? "http://localhost:3000";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<RustAdminPanelContext>(options => options.UseSqlite(dbConnectionString));
builder.Services.AddScoped<IEntityRepository<PlayerConnectionLog>, EntityRepository<PlayerConnectionLog>>();
builder.Services.AddScoped<IEntityRepository<ChatMessage>, EntityRepository<ChatMessage>>();
builder.Services.AddScoped<IEntityRepository<PlayerProfile>, EntityRepository<PlayerProfile>>();
builder.Services.AddScoped<IEntityRepository<PlayerReport>, EntityRepository<PlayerReport>>();

// Services
builder.Services.AddScoped<IPlayerConnectionsService, PlayerConnectionsService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ISteamService, SteamService>();
builder.Services.AddScoped<IPlayerReportsService, PlayerReportsService>();

// API-KEY
builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<IApiKeyValidator, ApiKeyValidator>();

// CORS
var RustAdminPanelCors = "RustAdminPanel_AllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(RustAdminPanelCors,
        policy =>
        {
            policy.WithOrigins(corsUrls)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Database migration
var dbOptions = new DbContextOptionsBuilder<RustAdminPanelContext>();
dbOptions.UseSqlite(dbConnectionString);

var dbContext = new RustAdminPanelContext(dbOptions.Options);
dbContext.Database.Migrate();

// Run App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseCors(RustAdminPanelCors); 

app.UseAuthorization();

app.MapControllers();

app.Run();
