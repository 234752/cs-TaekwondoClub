using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;

namespace MobileMAUI;

public class MainViewModel
{
    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Payment> Payments { get; set; }
    private RestService _restService { get; set; }
    public MainViewModel(RestService restService)
    {
        _restService = restService;
        Customers = new ObservableCollection<Customer>(restService.Customers);
        Events = new ObservableCollection<Event>(restService.Events);
        Payments = new ObservableCollection<Payment>(restService.Payments);
        StartDate = DateTime.Now;
        EndDate = StartDate.AddDays(30);
        FilterEventsByDate();
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void FilterEventsByDate()
    {
        Events.Clear();
        foreach (var e in _restService.Events.Where(e => e.Date >= StartDate && e.Date <= EndDate))
        {
            Events.Add(e);
        }
    }
}
