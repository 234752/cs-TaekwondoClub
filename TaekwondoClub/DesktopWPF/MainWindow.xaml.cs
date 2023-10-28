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
using DesktopWPF.ViewModels;
using DesktopWPF.Views;
using Microsoft.EntityFrameworkCore;

namespace DesktopWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DataContext _dataContext;
    private DbContextOptionsBuilder<DataContext> _optionsBuilder;
    public ObservableCollection<Customer> Customers { get; set; }
    public ObservableCollection<Event> Events { get; set; }
    public ObservableCollection<Payment> Payments { get; set; }
    public MainWindow()
    {
        _optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        _optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["pogconnectionstring2"].ConnectionString);
        _dataContext = new DataContext(_optionsBuilder.Options);
        _dataContext.Customers.Load();
        _dataContext.Events.Include(e => e.Customers).Load();
        _dataContext.Payments.Include(p => p.Customer).Load();
        Customers = _dataContext.Customers.Local.ToObservableCollection();
        Events = _dataContext.Events.Local.ToObservableCollection();
        Payments = _dataContext.Payments.Local.ToObservableCollection();

        InitializeComponent();
    }

    private void ReloadData()
    {
        _dataContext = new DataContext(_optionsBuilder.Options);
        _dataContext.Customers.Load();
        _dataContext.Events.Load();
        _dataContext.Payments.Load();
        Customers = _dataContext.Customers.Local.ToObservableCollection();
        Events = _dataContext.Events.Local.ToObservableCollection();
        Payments = _dataContext.Payments.Local.ToObservableCollection();
    }

    public async Task SaveChangesToDatabase()
    {
        await _dataContext.SaveChangesAsync();
    }

    private void ShowCustomersView(object sender, RoutedEventArgs e)
    {
        mainFrame.NavigationService.Navigate(new CustomerView(new CustomerViewModel(this, Customers)));
    }

    private void ShowEventsView(object sender, RoutedEventArgs e)
    {
        mainFrame.NavigationService.Navigate(new EventView(new EventViewModel(this, Events, Customers)));
    }

    private void ShowPaymentsView(object sender, RoutedEventArgs e)
    {
        mainFrame.NavigationService.Navigate(new PaymentView(new PaymentViewModel(this, Payments, Customers)));
    }

    public void ReloadCustomers()
    {
        ReloadData();
        mainFrame.NavigationService.Navigate(new CustomerView(new CustomerViewModel(this, Customers)));
    }

    public void ReloadEvents()
    {
        ReloadData();
        mainFrame.NavigationService.Navigate(new EventView(new EventViewModel(this, Events, Customers)));
    }
    public void ReloadPayments()
    {
        ReloadData();
        mainFrame.NavigationService.Navigate(new PaymentView(new PaymentViewModel(this, Payments, Customers)));
    }
}
