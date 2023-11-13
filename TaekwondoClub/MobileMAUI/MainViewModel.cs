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
        DaysList = Enumerable.Range(1, 30).Select(i => i.ToString()).ToList();
        DaysList.Add("unlimited");
    }
    public string Days { get; set; } = "unlimited";
    public List<string> DaysList { get; set; }
    public void FilterEventsByDate()
    {
        if (Days == "unlimited")
        {
            Events.Clear();
            foreach (var e in _restService.Events)
            {
                Events.Add(e);
            }
            return;
        }

        var currentTime = DateTime.Now;
        var days = int.Parse(Days);

        Events.Clear();
        foreach (var e in _restService.Events.Where(e => e.Date >= currentTime && e.Date <= currentTime.AddDays(days)))
        {
            Events.Add(e);
        }
    }
}
