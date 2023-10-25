﻿using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.ViewModels;

public class PaymentViewModel : BaseViewModel
{
    public ObservableCollection<Payment> Payments { get; set; }
    public ObservableCollection<Customer> Customers { get; set; }
    public PaymentViewModel(MainWindow mainWindow, ObservableCollection<Payment> payments, ObservableCollection<Customer> customers) : base(mainWindow)
    {
        Payments = payments;
        Customers = customers;
    }
    public async Task SavePaymentsToDatabase()
    {
        await MainWindow.SaveChangesToDatabase();
    }

    public void ReloadPayments()
    {
        MainWindow.ReloadPayments();
    }

    public void AddPayment(Payment payment)
    {
        MainWindow.Payments.Add(payment);
    }

    public void RemovePayment(Payment payment)
    {
        MainWindow.Payments.Remove(payment);
    }
}
