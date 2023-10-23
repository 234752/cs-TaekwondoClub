using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("pogconnectionstring2"));
});

var app = builder.Build();

app.MapGet("/customers", async (DataContext db) => 
    await db.Customers.ToListAsync());

app.Run();