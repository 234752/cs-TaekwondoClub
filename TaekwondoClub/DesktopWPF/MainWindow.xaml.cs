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
    public ObservableCollection<Customer> Customers;
    public MainWindow()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["pogconnectionstring2"].ConnectionString);
        _dataContext = new DataContext(optionsBuilder.Options);
        _dataContext.Customers.Load();
        Customers = _dataContext.Customers.Local.ToObservableCollection();

        InitializeComponent();
    }

    public async Task SaveChangesToDatabase()
    {
        _dataContext.SaveChangesAsync();
    }

    private void ShowCustomersView(object sender, RoutedEventArgs e)
    {
        mainFrame.NavigationService.Navigate(new CustomerView(new CustomerViewModel(this, Customers)));
    }
}
