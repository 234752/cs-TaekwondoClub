using DB.Entities;
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
public partial class CustomerView : Page
{
    public CustomerViewModel CustomerViewModel;
    public CustomerView(CustomerViewModel customerViewModel)
    {
        CustomerViewModel = customerViewModel;
        this.DataContext = customerViewModel.Customers;
        InitializeComponent();
    }

    private void saveButton_Click(object sender, RoutedEventArgs e)
    {
        CustomerViewModel.SaveCustomersToDatabase();
    }
}
