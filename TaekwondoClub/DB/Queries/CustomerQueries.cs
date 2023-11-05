using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Queries;

public static class CustomerQueries
{
    public static async Task<List<Customer>> CustomersWithDuePayments(DataContext context)
    {
        var payments = await context.Payments.Where(p => p.Paid == "no").Include(p => p.Customer).ToListAsync();
        var customers = payments.Select(p => p.Customer).ToList();
        return customers;
    }
}
