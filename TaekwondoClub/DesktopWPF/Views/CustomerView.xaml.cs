﻿using DB.Entities;
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

    private async void saveButton_Click(object sender, RoutedEventArgs e)
    {
        await CustomerViewModel.SaveCustomersToDatabase();
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
        CustomerViewModel.ReloadCustomers();
        
    }

    private void removeButton_Click(object sender, RoutedEventArgs e)
    {
        var customer = customerDetailsBorder.DataContext as Customer;
        CustomerViewModel.RemoveCustomer(customer);
    }

    private void saveNewCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        CustomerViewModel.AddCustomer(new Customer() 
        { 
            Name = newNameTextBox.Text,
            Surname = newSurnameTextBox.Text,
            Email = newEmailTextBox.Text,
            Address = newAddressTextBox.Text,
            AccountNumber = newAccountNumberTextBox.Text
        });
    }
    private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!RegexValidator.IsNumeric(e.Text))
        {
            e.Handled = true;
        }
    }

}
