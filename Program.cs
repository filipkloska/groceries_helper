using groceries_helper.Api;
using groceries_helper.Persistence;
using groceries_helper.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Test");
builder.Services.AddDbContext<GroceryHelperDbContext>((sp, options) => options
    .UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history");
    })
    .UseSnakeCaseNamingConvention()
);
builder.Services.AddScoped<IngredientService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<GroceryHelperDbContext>();
context!.Database.Migrate();
app.UseHttpsRedirection();
app.RegisterApiEndpoints();

app.Run();

