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
        NewEvent = new() { Date = DateTime.UtcNow };
        newEventRightCustomers = new ObservableCollection<Customer>(Customers);
        newEventLeftCustomers = new ObservableCollection<Customer>();
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
                if(selectedEvent == null) 
                {
                    selectedEventLeftCustomers = null;
                    selectedEventRightCustomers = null;
                    OnPropertyChanged("SelectedEventLeftCustomers");
                    OnPropertyChanged("SelectedEventRightCustomers");
                }
                else
                {
                    selectedEventLeftCustomers = new ObservableCollection<Customer>(selectedEvent.Customers);
                    selectedEventRightCustomers = new ObservableCollection<Customer>(Customers.Except(selectedEvent.Customers));
                    OnPropertyChanged("SelectedEventLeftCustomers");
                    OnPropertyChanged("SelectedEventRightCustomers");                
                }
            }
        }
    }
    public Customer LeftSelectedCustomer {  get; set; }
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
    private ObservableCollection<Customer> newEventRightCustomers;
    public ObservableCollection<Customer> NewEventRightCustomers
    {
        get { return newEventRightCustomers; }
        set
        {
            if (newEventRightCustomers != value)
            {
                newEventRightCustomers = value;
            }
        }
    }

    private ObservableCollection<Customer> newEventLeftCustomers;
    public ObservableCollection<Customer> NewEventLeftCustomers
    {
        get { return newEventLeftCustomers; }
        set
        {
            if (newEventLeftCustomers != value)
            {
                newEventLeftCustomers = value;
            }
        }
    }

    public Customer NewLeftCustomer { get; set; }
    public Customer NewRightCustomer { get; set; }

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
        var ev = new Event(NewEvent);
        //ev.Customers = NewEventLeftCustomers.ToList();
        MainWindow.Events.Add(ev);
    }

    public void RemoveEvent(Event ev)
    {
        MainWindow.Events.Remove(ev);
    }
}


