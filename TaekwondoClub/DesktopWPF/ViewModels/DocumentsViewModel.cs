using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;

namespace DesktopWPF.ViewModels;

public class DocumentsViewModel
{
    public List<Customer> Customers { get; set; }
    public List<Event> Events { get; set; }
    public List<Payment> Payments { get; set; }
    public List<Attendance> Attendances { get; set; }

    public DocumentsViewModel(List<Customer> customers, List<Event> events, List<Payment> payments, List<Attendance> attendances)
    {
        Customers = customers;
        Events = events;
        Payments = payments;
        Attendances = attendances;
    }
}
