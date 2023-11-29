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
        private class MinimalViewModel()
        {
            public Event Event { get; set; }
            public ObservableCollection<int> HourOptions { get; set; }
            public ObservableCollection<int> MinuteOptions { get; set; }
            public int SelectedHour { get; set; }
            public int SelectedMinute { get; set; }
            public bool IsDetailedMinutes { get; set; }
            public List<Customer> Customers { get; set; }
        }

        public EventDetailView(Event ev)
        {
            InitializeComponent();

            model = new MinimalViewModel
            {
                Event = ev,
                SelectedHour = ev.Date.Hour,
                SelectedMinute = ev.Date.Minute,
                IsDetailedMinutes = true
            };
            DataContext = model;
            model.HourOptions = new ObservableCollection<int>();
            model.MinuteOptions = new ObservableCollection<int>();
            GenerateHourOptions();
            GenerateMinuteOptions();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDateTime = (DateTime)model.Event.Date;
            selectedDateTime = selectedDateTime.Date + TimeSpan.FromHours(model.SelectedHour) + TimeSpan.FromMinutes(model.SelectedMinute);

            model.Event.Date = selectedDateTime;

            SaveChanges = true;
            Close();
        }

        private void GenerateHourOptions()
        {
            model.HourOptions.Clear();
            for (int hour = 0; hour < 24; hour++)
            {
                model.HourOptions.Add(hour);
            }
        }

        private void GenerateMinuteOptions()
        {
            model.MinuteOptions.Clear();
            if (model.IsDetailedMinutes)
            {
                for (int minute = 0; minute < 60; minute++)
                {
                    model.MinuteOptions.Add(minute);
                }
            }
            else
            {
                for (int minute = 0; minute < 60; minute += 15)
                {
                    model.MinuteOptions.Add(minute);
                }
            }
        }

        private void IsDetailedMinutesToggleButton_Checked(object sender, RoutedEventArgs e)
        {            
            GenerateMinuteOptions();
        }
    }
}
