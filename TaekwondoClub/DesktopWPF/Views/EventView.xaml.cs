﻿using DB.Entities;
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
    }

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
}
