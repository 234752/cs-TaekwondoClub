using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Queries;

public static class EventQueries
{
    public static async Task<List<Event>> UpcomingEvents(DataContext context, int days)
    {
        var events = await context.Events.Where(e => e.Date < DateTime.Now.AddDays(days)).Include(e => e.Customers).ToListAsync();
        return events;
    }
}
