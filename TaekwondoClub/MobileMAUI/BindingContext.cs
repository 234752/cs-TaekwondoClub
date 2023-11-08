using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;

namespace MobileMAUI;

public class BindingContext
{
    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Payment> Payments { get; set; }
    public BindingContext(RestService restService)
    {
        Customers = new ObservableCollection<Customer>(restService.Customers);
        Events = new ObservableCollection<Event>(restService.Events);
        Payments = new ObservableCollection<Payment>(restService.Payments);
    }
}
