﻿using DB.Entities;
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
        eventDetailsMoveRightButton.Visibility = Visibility.Collapsed;
        eventDetailsMoveLeftButton.Visibility = Visibility.Collapsed;
        newEventDetailsMoveRightButton.Visibility = Visibility.Collapsed;
        newEventDetailsMoveLeftButton.Visibility = Visibility.Collapsed;
    }
    private bool isLeftListViewChanging = false;
    private bool isRightListViewChanging = false;
    private bool isNewLeftListViewChanging = false;
    private bool isNewRightListViewChanging = false;

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
    private void RemoveEventButton_Click(object sender, RoutedEventArgs e)
    {
        var ev = eventDetailsBorder.DataContext as Event;
        EventViewModel.RemoveEvent(ev);
    }

    private void EditEventButton_Click(object sender, RoutedEventArgs e)
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
        var customerToBeMoved = EventViewModel.NewLeftCustomer;

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
    private void newEventNewLeftCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isNewRightListViewChanging)
        {
            isNewLeftListViewChanging = true;
            newEventRightCustomers.SelectedItem = null;
            isNewLeftListViewChanging = false;
        }
        if (newEventLeftCustomers.SelectedItem != null)
        {
            newEventDetailsMoveRightButton.Visibility = Visibility.Visible;
        }
        else
        {
            newEventDetailsMoveRightButton.Visibility = Visibility.Collapsed;
        }
    }
    private void newEventNewRightCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isNewLeftListViewChanging)
        {
            isNewRightListViewChanging = true;
            newEventLeftCustomers.SelectedItem = null;
            isNewRightListViewChanging = false;
        }
        if (newEventRightCustomers.SelectedItem != null)
        {
            newEventDetailsMoveLeftButton.Visibility = Visibility.Visible;
        }
        else
        {
            newEventDetailsMoveLeftButton.Visibility = Visibility.Collapsed;
        }
    }
    //new implementation

    private void AddEventButton_Click(object sender, RoutedEventArgs e)
    {
        Event newEvent = EventViewModel.NewEvent;
        var eventDetailView = new EventDetailView(newEvent);
        eventDetailView.ShowDialog();
        if (eventDetailView.SaveChanges)
        {
            EventViewModel.AddEvent();
        }
    }
/*    private void EditPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        Payment selectedPayment = (Payment)paymentListView.SelectedItem;
        if (selectedPayment != null)
        {
            var editedPayment = new Payment(selectedPayment);
            var paymentDetailView = new PaymentDetailView(editedPayment, PaymentViewModel.Customers.ToList());
            paymentDetailView.ShowDialog();
            if (paymentDetailView.SaveChanges)
            {
                selectedPayment.ReplaceProperties(editedPayment);
                paymentListView.Items.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Please select a payment to edit.");
        }
    }
    private void RemovePaymentButton_Click(object sender, RoutedEventArgs e)
    {
        Payment selectedPayment = (Payment)paymentListView.SelectedItem;
        if (selectedPayment != null)
        {
            PaymentViewModel.RemovePayment(selectedPayment);
        }
        else
        {
            MessageBox.Show("Please select a payment to delete.");
        }
    }*/
}
