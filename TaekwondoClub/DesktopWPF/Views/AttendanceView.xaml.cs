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

        private void SaveAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges = true;
            Close();
        }
    }
}
