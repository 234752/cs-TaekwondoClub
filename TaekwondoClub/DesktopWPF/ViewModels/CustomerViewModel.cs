using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.ViewModels;

public class CustomerViewModel : BaseViewModel
{
    public ObservableCollection<Customer> Customers { get; set; }
    public CustomerViewModel(MainWindow mainWindow, ObservableCollection<Customer> customers) : base(mainWindow)
    {
        Customers = customers;
        NewCustomer = new Customer();
    }
    public Customer NewCustomer { get; set; }
    public async Task SaveCustomersToDatabase()
    {
        await MainWindow.SaveChangesToDatabase();
    }

    public void ReloadCustomers()
    {
       MainWindow.ReloadCustomers();   
    }

    public void AddCustomer(Customer customer)
    {
        MainWindow.Customers.Add(customer);
    }

    public void RemoveCustomer(Customer customer) 
    {
        MainWindow.Customers.Remove(customer);
    }

}
