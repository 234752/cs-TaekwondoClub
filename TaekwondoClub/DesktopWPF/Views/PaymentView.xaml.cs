using DB.Entities;
using DesktopWPF.Validators;
using DesktopWPF.ViewModels;
using System;
using System.Collections.Generic;
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
/// Interaction logic for PaymentView.xaml
/// </summary>
public partial class PaymentView : Page
{
    public PaymentViewModel PaymentViewModel { get; set; }
    public PaymentView(PaymentViewModel paymentViewModel)
    {
        PaymentViewModel = paymentViewModel;
        this.DataContext = PaymentViewModel;
        InitializeComponent();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        button.IsEnabled = false;
        await PaymentViewModel.SavePaymentsToDatabase();
        button.IsEnabled = true;
    }

    private void RevertButton_Click(object sender, RoutedEventArgs e)
    {
        PaymentViewModel.ReloadPayments();
    }
    private void AddPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        Payment newPayment = PaymentViewModel.NewPayment;
        var paymentDetailView = new PaymentDetailView(newPayment, PaymentViewModel.Customers.ToList());
        paymentDetailView.ShowDialog();
        if (paymentDetailView.SaveChanges)
        {
            PaymentViewModel.AddPayment();
        }
    }
    private void EditPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        Payment selectedPayment = (Payment)paymentListView.SelectedItem;
        if (selectedPayment != null)
        {
            var editedPayment = new Payment(selectedPayment);
            var paymentDetailView = new PaymentDetailView(editedPayment, PaymentViewModel.Customers.ToList());
            paymentDetailView.ShowDialog();
            if (paymentDetailView.SaveChanges)
            {
                selectedPayment.ReplaceProperties(editedPayment);
                paymentListView.Items.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Please select a payment to edit.");
        }
    }
    private void RemovePaymentButton_Click(object sender, RoutedEventArgs e)
    {
        Payment selectedPayment = (Payment)paymentListView.SelectedItem;
        if (selectedPayment != null)
        {
            PaymentViewModel.RemovePayment(selectedPayment);
        }
        else
        {
            MessageBox.Show("Please select a payment to delete.");
        }
    }


}
