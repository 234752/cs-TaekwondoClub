using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    }
    public async Task SaveCustomersToDatabase()
    {
        MainWindow.SaveChangesToDatabase();
    }

    public async Task ReloadCustomers()
    {
       MainWindow.ReloadCustomers();
    }

}
