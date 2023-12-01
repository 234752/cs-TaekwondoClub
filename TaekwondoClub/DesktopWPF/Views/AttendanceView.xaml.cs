using DB.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AttendanceView.xaml
    /// </summary>
    public partial class AttendanceView : Window
    {
        private MinimalViewModel model;
        public bool SaveChanges { get; set; }
        private class MinimalViewModel
        {
            public Event Event { get; set; }
            public List<Attendance> AttendingCustomers { get; set; }
            public MinimalViewModel(Event ev, List<Attendance> attendingCustomers)
            { 
                Event = ev;
                AttendingCustomers = attendingCustomers;
            }
        }
        public AttendanceView(Event selectedEvent, List<Attendance> attendingCustomers)
        {
            InitializeComponent();

            model = new MinimalViewModel(selectedEvent, attendingCustomers);
            DataContext = model;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            model.Event.Attendances = model.AttendingCustomers;
            SaveChanges = true;
            Close();
        }
        private void ToggleAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            var dataGrid = attendanceList as DataGrid;
            if (dataGrid.SelectedItem is Customer selectedCustomer)
            {
                var attendance = model.AttendingCustomers.FirstOrDefault(a => a.CustomerId == selectedCustomer.Id);

                if (attendance != null)
                {
                    model.AttendingCustomers.Remove(attendance);
                }
                else
                {
                    model.AttendingCustomers.Add(new Attendance { EventId = model.Event.Id, CustomerId = selectedCustomer.Id });
                }
                dataGrid.Items.Refresh();
            }
        }
        private void AttendanceListDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item is Customer customer)
            {
                bool isAttending = model.AttendingCustomers.Any(a => a.CustomerId == customer.Id);
                e.Row.Background = isAttending ? Brushes.LightGreen : Brushes.White;
            }
        }
    }
}
