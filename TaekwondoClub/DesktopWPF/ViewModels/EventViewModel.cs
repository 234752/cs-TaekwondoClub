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

public class EventViewModel : BaseViewModel
{
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Attendance> Attendances { get; set; }

    public EventViewModel(MainWindow mainWindow, ObservableCollection<Event> events, ObservableCollection<Customer> customers,
        ObservableCollection<Attendance> attendances) : base(mainWindow)
    {
        Events = events;
        Customers = customers;
        Attendances = attendances;
        NewEvent = new() { Date = DateTime.UtcNow, Type = "One Time" };
        Attendances = attendances;
    }
    public Event NewEvent { get; set; }

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
        ev.Customers = NewEvent.Customers;
        ev.Attendances = new();
        MainWindow.Events.Add(ev);
    }

    public void RemoveEvent(Event ev)
    {
        MainWindow.Events.Remove(ev);
    }
    public void UpdateAttendance(Event ev, List<Attendance> attendances)
    {
        var oldAttendance = Attendances.Where(a => a.EventId == ev.Id).ToList();
        var attendanceRecordsToAdd = attendances.Except(oldAttendance).ToList();
        var attendanceRecordsToRemove = oldAttendance.Except(attendances).ToList();
        foreach (var attendance in attendanceRecordsToAdd)
        {
            MainWindow.Attendances.Add(attendance);
        }
        foreach (var attendance in attendanceRecordsToRemove)
        {
            MainWindow.Attendances.Remove(attendance);
        }
    }
    public void AddClasses(List<Event> events)
    {
        foreach (var ev in events)
        {
            ev.Attendances = new();
            MainWindow.Events.Add(ev);
        }
    }
}


