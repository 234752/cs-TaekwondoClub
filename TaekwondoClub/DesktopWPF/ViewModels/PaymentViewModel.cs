using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        NewPayment = new() { Name = "", MonthYear = "00/0000", DueDate = DateTime.UtcNow, Paid = "no", CustomerId = Customers.First().Id };
    }
    private Payment selectedPayment;
    public Payment SelectedPayment
    {
        get { return selectedPayment; }
        set
        {
            if (selectedPayment != value)
            {
                selectedPayment = value;
            }
        }
    }
    private Payment newPayment;
    public Payment NewPayment
    {
        get { return newPayment; }
        set
        {
            if (newPayment != value)
            {
                newPayment = value;
            }
        }
    }
    public async Task SavePaymentsToDatabase()
    {
        await MainWindow.SaveChangesToDatabase();
    }

    public void ReloadPayments()
    {
        MainWindow.ReloadPayments();
    }

    public void AddPayment()
    {
        var payment = new Payment(NewPayment);
        payment.Customer = Customers.First(c => c.Id == payment.CustomerId);
        MainWindow.Payments.Add(payment);
    }

    public void RemovePayment(Payment payment)
    {
        MainWindow.Payments.Remove(payment);
    }
}
