using ApiParaPWA.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var connectionString = builder.Configuration.GetConnectionString("Products");
var databaseUrl = builder.Configuration["DATABASE_URL"] ?? connectionString;
if (databaseUrl?.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase) == true ||
    databaseUrl?.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) == true)
{
    var uri = new Uri(databaseUrl);
    var credentials = uri.UserInfo.Split(':', 2);
    connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.IsDefaultPort ? 5432 : uri.Port,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = Uri.UnescapeDataString(credentials[0]),
        Password = Uri.UnescapeDataString(credentials[1]),
        SslMode = SslMode.Require,
        ChannelBinding = ChannelBinding.Require
    }.ConnectionString;
}
builder.Services.AddDbContext<ProductDbContext>(options => options.UseNpgsql(connectionString));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.MapControllers();

app.Run();
