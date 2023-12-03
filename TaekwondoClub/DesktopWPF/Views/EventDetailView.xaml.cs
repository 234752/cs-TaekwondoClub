using DB.Entities;
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
using System.Windows.Shapes;

namespace DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for EventDetailView.xaml
    /// </summary>
    public partial class EventDetailView : Window
    {
        private MinimalViewModel model;
        public bool SaveChanges { get; set; } = false;
        private bool isUpdatingSelection = false;
        private class MinimalViewModel()
        {
            public Event Event { get; set; }
            public ObservableCollection<int> HourOptions { get; set; }
            public ObservableCollection<int> MinuteOptions { get; set; }
            public List<string> EventTypes { get; set; }
            public int SelectedHour { get; set; }
            public int SelectedMinute { get; set; }
            public bool IsDetailedMinutes { get; set; }
            public ObservableCollection<Customer> IncludedCustomers { get; set; }
            public ObservableCollection<Customer> ExcludedCustomers { get; set; }
            public Customer SelectedIncludedCustomer { get; set; }
            public Customer SelectedExcludedCustomer { get; set; }
        }

        public EventDetailView(Event ev, List<Customer> customers)
        {
            InitializeComponent();

            var excludedCustomers = customers.Except(ev.Customers);
            model = new MinimalViewModel
            {
                Event = ev,
                SelectedHour = ev.Date.Hour,
                SelectedMinute = ev.Date.Minute,
                IsDetailedMinutes = true,
                IncludedCustomers = new ObservableCollection<Customer>(ev.Customers),
                ExcludedCustomers = new ObservableCollection<Customer>(excludedCustomers),
                EventTypes = new List<string>() { "One Time", "Timetable Class", "Class" }
            };
            DataContext = model;
            model.HourOptions = new ObservableCollection<int>();
            model.MinuteOptions = new ObservableCollection<int>();
            for (int hour = 0; hour < 24; hour++)
            {
                model.HourOptions.Add(hour);
            }
            for (int minute = 0; minute < 60; minute++)
            {
                model.MinuteOptions.Add(minute);
            }

            excludeCustomerButton.IsEnabled = false;
            includeCustomerButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDateTime = (DateTime)model.Event.Date;
            selectedDateTime = selectedDateTime.Date + TimeSpan.FromHours(model.SelectedHour) + TimeSpan.FromMinutes(model.SelectedMinute);
            model.Event.Date = selectedDateTime;

            SaveChanges = true;
            Close();
        }
        private void IncludedCustomersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingSelection) return;

            isUpdatingSelection = true;
            excludedCustomersList.SelectedItem = null;
            isUpdatingSelection = false;
            UpdateButtonsState();

        }

        private void ExcludedCustomersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdatingSelection) return;

            isUpdatingSelection = true;
            includedCustomersList.SelectedItem = null;
            isUpdatingSelection = false;
            UpdateButtonsState();
        }

        private void IncludeCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = model.SelectedExcludedCustomer;
            model.Event.Customers.Add(customer);
            model.IncludedCustomers.Add(customer);
            model.ExcludedCustomers.Remove(customer);
            UpdateButtonsState();
        }

        private void ExcludeCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = model.SelectedIncludedCustomer;
            model.Event.Customers.Remove(customer);
            model.ExcludedCustomers.Add(customer);
            model.IncludedCustomers.Remove(customer);
            UpdateButtonsState();
        }
        private void UpdateButtonsState()
        {
            if (includedCustomersList.SelectedItem != null) excludeCustomerButton.IsEnabled = true;
            else excludeCustomerButton.IsEnabled = false;

            if (excludedCustomersList.SelectedItem != null) includeCustomerButton.IsEnabled = true;
            else includeCustomerButton.IsEnabled = false;
        }
    }
}
