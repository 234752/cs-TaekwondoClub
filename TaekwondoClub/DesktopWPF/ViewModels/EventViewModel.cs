using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.ViewModels;

public class EventViewModel : BaseViewModel, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Customer> Customers { get; set; }

    public EventViewModel(MainWindow mainWindow, ObservableCollection<Event> events, ObservableCollection<Customer> customers) : base(mainWindow)
    {
        Events = events;
        Customers = customers;
        NewEvent = new() { Name = "", Date = DateTime.UtcNow, Customers = new List<Customer> { Customers.First() } };
    }

    private Event selectedEvent;
    public Event SelectedEvent
    {
        get { return selectedEvent; }
        set
        {
            if (selectedEvent != value)
            {
                selectedEvent = value;
                selectedEventLeftCustomers = new ObservableCollection<Customer>(selectedEvent.Customers);
                selectedEventRightCustomers = new ObservableCollection<Customer>(Customers.Except(selectedEvent.Customers));
                OnPropertyChanged("SelectedEventLeftCustomers");
                OnPropertyChanged("SelectedEventRightCustomers");                
            }
        }
    }
    private Customer leftSelectedCustomer;
    public Customer LeftSelectedCustomer 
    {
        get { return leftSelectedCustomer; } 
        set
        {
            leftSelectedCustomer = value;
        }
    }
    public Customer RightSelectedCustomer { get; set; }

    private ObservableCollection<Customer> selectedEventRightCustomers;
    public ObservableCollection<Customer> SelectedEventRightCustomers
    {
        get { return selectedEventRightCustomers; }
        set
        {
            if (selectedEventRightCustomers != value)
            {
                selectedEventRightCustomers = value;
            }
        }
    }

    private ObservableCollection<Customer> selectedEventLeftCustomers;
    public ObservableCollection<Customer> SelectedEventLeftCustomers
    {
        get { return selectedEventLeftCustomers; }
        set
        {
            if (selectedEventLeftCustomers != value)
            {
                selectedEventLeftCustomers = value;
            }
        }
    }

    private Event newEvent;

    public Event NewEvent
    {
        get { return newEvent; }
        set
        {
            if (newEvent != value)
            {
                newEvent = value;
            }
        }
    }

    public async Task SaveEventsToDatabase()
    {
        await MainWindow.SaveChangesToDatabase();
    }

    public void ReloadEvents()
    {
        MainWindow.ReloadEvents();
    }

    public void AddEvent()
    {
        var ev = NewEvent;
        ev.Customers = new List<Customer> { Customers.First(c => c.Id == ev.Customers.First().Id) };
        MainWindow.Events.Add(ev);
    }

    public void RemoveEvent(Event ev)
    {
        MainWindow.Events.Remove(ev);
    }
}


