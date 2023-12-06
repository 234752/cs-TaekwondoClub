using DB.Entities;
using DesktopWPF.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopWPF.Views;

/// <summary>
/// Interaction logic for CustomerView.xaml
/// </summary>
public partial class EventView : Page
{
    public EventViewModel EventViewModel;
    public EventView(EventViewModel eventViewModel)
    {
        EventViewModel = eventViewModel;
        this.DataContext = EventViewModel;        
        InitializeComponent();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        button.IsEnabled = false;
        await EventViewModel.SaveEventsToDatabase();
        button.IsEnabled = true;
    }

    private void RevertButton_Click(object sender, RoutedEventArgs e)
    {
        EventViewModel.ReloadEvents();
    }
    
    private void AddEventButton_Click(object sender, RoutedEventArgs e)
    {
        Event newEvent = EventViewModel.NewEvent;
        newEvent.Customers = new();
        var eventDetailView = new EventDetailView(newEvent, EventViewModel.Customers.ToList());
        eventDetailView.ShowDialog();
        if (eventDetailView.SaveChanges)
        {            
            EventViewModel.AddEvent();
        }
    }
    private void EditEventButton_Click(object sender, RoutedEventArgs e)
    {
        Event selectedEvent = (Event)eventListView.SelectedItem;
        if (selectedEvent != null)
        {
            var editedEvent = new Event(selectedEvent);
            editedEvent.Customers = new List<Customer>(selectedEvent.Customers);
            var eventDetailView = new EventDetailView(editedEvent, EventViewModel.Customers.ToList());
            eventDetailView.ShowDialog();
            if (eventDetailView.SaveChanges)
            {
                selectedEvent.ReplaceProperties(editedEvent);
                selectedEvent.Customers = editedEvent.Customers;
                eventListView.Items.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Please select an event to edit.");
        }
    }
    private void RemoveEventButton_Click(object sender, RoutedEventArgs e)
    {
        Event selectedEvent = (Event)eventListView.SelectedItem;
        if (selectedEvent != null)
        {
            EventViewModel.RemoveEvent(selectedEvent);
        }
        else
        {
            MessageBox.Show("Please select an event to delete.");
        }
    }
    private void CheckAttendanceButton_Click(object sender, RoutedEventArgs e)
    {
        Event selectedEvent = (Event)eventListView.SelectedItem;
        if (selectedEvent != null)
        {
            var editedEvent = new Event(selectedEvent);
            editedEvent.Customers = new List<Customer>(selectedEvent.Customers);
            editedEvent.Attendances = new List<Attendance>(selectedEvent.Attendances);
            var attendanceView = new AttendanceView(editedEvent, editedEvent.Attendances);
            attendanceView.ShowDialog();
            if(attendanceView.SaveChanges) 
            {
                selectedEvent.Attendances = editedEvent.Attendances;
                //EventViewModel.UpdateAttendance(selectedEvent, editedEvent.Attendances);
            }
        }
        else
        {
            MessageBox.Show("Please select an event to check the attendance.");
        }
    }

    private void AddClassesButton_Click(object sender, RoutedEventArgs e)
    {
        Event selectedEvent = (Event)eventListView.SelectedItem;
        if (selectedEvent != null && selectedEvent.Type == "Timetable Class")
        {
            var classes = new List<Event>();
            var classesView = new ClassesView(selectedEvent, classes);
            classesView.ShowDialog();
            if (classesView.SaveChanges)
            {
                EventViewModel.AddClasses(classes);
            }
        }
        else
        {
            MessageBox.Show("Please select an 'Timetable Class' event to add reccuring classes");
        }
    }
}
