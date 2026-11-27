using Eelarveanaluutik.Api.Data;
using Eelarveanaluutik.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration, builder.Environment);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (args.Contains("--check-database"))
{
    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
    if (!await database.Database.CanConnectAsync())
    {
        throw new InvalidOperationException("Cannot connect to PostgreSQL. Check the local connection configuration.");
    }
    Console.WriteLine("PostgreSQL connection successful.");
    return;
}

app.MapControllers();

app.Run();
