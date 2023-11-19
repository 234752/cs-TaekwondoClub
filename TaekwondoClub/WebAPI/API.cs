using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using DB;
using DB.Queries;
using WebAPI;
using DB.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("pogconnectionstring2"));
});
builder.Services.AddScoped<EmailService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var emailConfig = configuration.GetSection("EmailConfig").Get<EmailConfig>();

    return new EmailService
    {
        SmtpServer = emailConfig.SmtpServer,
        SmtpPort = emailConfig.SmtpPort,
        UserName = emailConfig.UserName,
        Password = emailConfig.Password
    };
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

app.MapPost("/email/duepayments", (List<Payment> payments, EmailService emailService) =>
{
    try
    {
        foreach (var payment in payments)
        {
            var dto = EmailWriter.UnpaidPaymentNotificationEmail(payment);
            emailService.SendEmail(dto);
        }
        return Results.Ok("Email sent successfully");
    }
    catch (Exception ex)
    {
        return Results.StatusCode(500);
    }
});

app.MapPost("/email/eventparticipants", (List<Event> events, EmailService emailService) =>
{
    try
    {
        foreach (var e in events)
        {
            foreach (var c in e.Customers)
            {
                var dto = EmailWriter.EventParticipationNotificationEmail(e, c);
                emailService.SendEmail(dto);
            }
        }
        return Results.Ok("Email sent successfully");
    }
    catch (Exception ex)
    {
        return Results.StatusCode(500);
    }
});

app.Run();