using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using DB;
using DB.Queries;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("pogconnectionstring2"));
});

var app = builder.Build();

app.MapGet("/customers", async (DataContext db) => 
    await db.Customers.ToListAsync());
app.MapGet("/events", async (DataContext db) =>
    await db.Events.Include(e => e.Customers).ToListAsync());
app.MapGet("/payments", async (DataContext db) =>
    await db.Payments.Include(p => p.Customer).ToListAsync());

app.MapGet("/customerswithduepayments", async (DataContext db) =>
{
    return await CustomerQueries.CustomersWithDuePayments(db);
});

app.MapGet("/upcomingevents/{days}", async (int days, DataContext db) =>
{
    return await EventQueries.UpcomingEvents(db, days);
});

app.Run();