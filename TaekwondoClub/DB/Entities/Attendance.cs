using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities;

public class Attendance
{
    public int Id { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
