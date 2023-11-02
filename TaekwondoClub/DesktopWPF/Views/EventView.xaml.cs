using DB.Entities;
using DesktopWPF.ViewModels;
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
        eventDetailsMoveLeftButton.Visibility = Visibility.Collapsed;
        eventDetailsMoveRightButton.Visibility = Visibility.Collapsed;
    }
    private bool isLeftListViewChanging = false;
    private bool isRightListViewChanging = false;

    private void saveButton_Click(object sender, RoutedEventArgs e)
    {
        EventViewModel.SaveEventsToDatabase();
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
        EventViewModel.ReloadEvents();
    }
    private void removeEventButton_Click(object sender, RoutedEventArgs e)
    {
        var ev = eventDetailsBorder.DataContext as Event;
        EventViewModel.RemoveEvent(ev);
    }

    private void saveNewEventButton_Click(object sender, RoutedEventArgs e)
    {
        EventViewModel.AddEvent();
    }

    private void eventDetailsMoveLeftButton_Click(object sender, RoutedEventArgs e)
    {
        var customerToBeMoved = EventViewModel.RightSelectedCustomer;

        EventViewModel.SelectedEvent.Customers.Add(customerToBeMoved);
        EventViewModel.SelectedEventLeftCustomers.Add(customerToBeMoved);

        EventViewModel.SelectedEventRightCustomers.Remove(customerToBeMoved);

        EventViewModel.LeftSelectedCustomer = customerToBeMoved;
        EventViewModel.RightSelectedCustomer = null;
    }

    private void eventDetailsMoveRightButton_Click(object sender, RoutedEventArgs e)
    {
        var customerToBeMoved = EventViewModel.LeftSelectedCustomer;

        EventViewModel.SelectedEvent.Customers.Remove(customerToBeMoved);
        EventViewModel.SelectedEventLeftCustomers.Remove(customerToBeMoved);

        EventViewModel.SelectedEventRightCustomers.Add(customerToBeMoved);

        EventViewModel.RightSelectedCustomer = customerToBeMoved;
        EventViewModel.LeftSelectedCustomer = null;
    }

    private void newEventDetailsMoveLeftButton_Click(object sender, RoutedEventArgs e)
    {
        var customerToBeMoved = EventViewModel.NewRightCustomer;

        //EventViewModel.NewEvent.Customers.Add(customerToBeMoved);
        EventViewModel.NewEventLeftCustomers.Add(customerToBeMoved);

        EventViewModel.NewEventRightCustomers.Remove(customerToBeMoved);

        EventViewModel.NewLeftCustomer = customerToBeMoved;
        EventViewModel.NewRightCustomer = null;
    }

    private void newEventDetailsMoveRightButton_Click(object sender, RoutedEventArgs e)
    {
        var customerToBeMoved = EventViewModel.LeftSelectedCustomer;

        //EventViewModel.NewEvent.Customers.Remove(customerToBeMoved);
        EventViewModel.NewEventLeftCustomers.Remove(customerToBeMoved);

        EventViewModel.NewEventRightCustomers.Add(customerToBeMoved);

        EventViewModel.NewRightCustomer = customerToBeMoved;
        EventViewModel.NewLeftCustomer = null;
    }
    private void selectedEventLeftCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isRightListViewChanging)
        {
            isLeftListViewChanging = true;
            selectedEventRightCustomers.SelectedItem = null;
            isLeftListViewChanging = false;
        }
        if (selectedEventLeftCustomers.SelectedItem != null)
        {
            eventDetailsMoveRightButton.Visibility = Visibility.Visible;
        }
        else
        {
            eventDetailsMoveRightButton.Visibility = Visibility.Collapsed;
        }
    }
    private void selectedEventRightCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isLeftListViewChanging)
        {
            isRightListViewChanging = true;
            selectedEventLeftCustomers.SelectedItem = null;
            isRightListViewChanging = false;
        }
        if (selectedEventRightCustomers.SelectedItem != null)
        {
            eventDetailsMoveLeftButton.Visibility = Visibility.Visible;
        }
        else
        {
            eventDetailsMoveLeftButton.Visibility = Visibility.Collapsed;
        }
    }
}
