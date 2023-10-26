using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class Payment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MonthYear { get; set; }
    public DateTime DueDate { get; set; }
    public string Paid { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
