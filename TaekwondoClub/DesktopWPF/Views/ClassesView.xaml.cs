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

namespace DesktopWPF.Views;

/// <summary>
/// Interaction logic for ClassesView.xaml
/// </summary>
public partial class ClassesView : Window
{
    private MinimalViewModel model;
    public bool SaveChanges { get; set; } = false;
    private class MinimalViewModel()
    {
        public Event Event { get; set; }
        public List<Event> Classes { get; set; }
        public DateTime EndDate { get; set; }
    }

    public ClassesView(Event ev, List<Event> classes)
    {
        InitializeComponent();

        model = new MinimalViewModel
        {
            Event = ev,
            Classes = classes,
            EndDate = ev.Date.AddMonths(3).AddDays(1),
        };
        DataContext = model;
        endDateDatePicker.DisplayDateStart = ev.Date;

    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        for(var date = model.Event.Date; date < model.EndDate; date = date.AddDays(7))
        {
            var newClass = new Event()
            {
                Name = model.Event.Name,
                Date = date,
                Type = "Class",
                Customers = model.Event.Customers
            };
            model.Classes.Add(newClass);
        }
        SaveChanges = true;
        Close();
    }
}
