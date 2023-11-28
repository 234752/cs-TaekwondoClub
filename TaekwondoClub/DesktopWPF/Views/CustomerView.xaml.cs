using DB.Entities;
using DesktopWPF.ViewModels;
using DesktopWPF.Validators;
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

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        button.IsEnabled = false;
        await CustomerViewModel.SaveCustomersToDatabase();
        button.IsEnabled = true;
    }

    private void RevertButton_Click(object sender, RoutedEventArgs e)
    {
        CustomerViewModel.ReloadCustomers();
        
    }
    private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        Customer newCustomer = CustomerViewModel.NewCustomer;
        var customerDetailView = new CustomerDetailView(newCustomer);
        customerDetailView.ShowDialog();
        if(customerDetailView.SaveChanges) 
        {
            CustomerViewModel.AddCustomer();
        }
        CustomerViewModel.NewCustomer = new Customer();
    }
    private void EditCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        Customer selectedCustomer = (Customer)customerListView.SelectedItem;        
        if (selectedCustomer != null)
        {
            var editedCustomer = new Customer(selectedCustomer);
            var customerDetailView = new CustomerDetailView(editedCustomer);
            customerDetailView.ShowDialog();
            if(customerDetailView.SaveChanges) 
            {
                selectedCustomer.ReplaceProperties(editedCustomer);
                customerListView.Items.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Please select a customer to edit.");
        }
    }
    private void RemoveCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        Customer selectedCustomer = (Customer)customerListView.SelectedItem;
        if (selectedCustomer != null)
        {
            CustomerViewModel.RemoveCustomer(selectedCustomer);
        }
        else
        {
            MessageBox.Show("Please select a customer to delete.");
        }
    }

}
