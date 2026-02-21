using Microsoft.EntityFrameworkCore;
using RustAdminPanel.API.ApiKey;
using RustAdminPanel.DAL.Context;
using RustAdminPanel.DAL.Repositories;
using RustAdminPanel.Domain.Entities;
using RustAdminPanel.Services.ChatMessages;
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
builder.Services.AddScoped<IEntityRepository<ChatMessage>, EntityRepository<ChatMessage>>();

// Services
builder.Services.AddScoped<IPlayerConnectionsService, PlayerConnectionsService>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();

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
            policy.WithOrigins("http://localhost:3000")
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
