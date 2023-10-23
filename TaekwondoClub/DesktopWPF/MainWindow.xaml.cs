using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using DB;
using DB.Entities;
using DesktopWPF.Views;
using Microsoft.EntityFrameworkCore;

namespace DesktopWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Customer> Customers;
    public MainWindow()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["pogconnectionstring2"].ConnectionString);
        var dbContext = new DataContext(optionsBuilder.Options);
        dbContext.Customers.Load();
        Customers = dbContext.Customers.Local.ToObservableCollection();

        InitializeComponent();
    }

    private void ShowCustomersView(object sender, RoutedEventArgs e)
    {
        mainFrame.NavigationService.Navigate(new CustomerView(Customers));
    }
}
