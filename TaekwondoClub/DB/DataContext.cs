using Microsoft.EntityFrameworkCore;
using System.Configuration;
using DB.Entities;

namespace DB;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["pogconnectionstring2"].ConnectionString);
    }*/

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Payment> Payments { get; set; }

}