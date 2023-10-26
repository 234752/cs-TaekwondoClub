using DB.Entities;
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

    private async void saveButton_Click(object sender, RoutedEventArgs e)
    {
        await PaymentViewModel.SavePaymentsToDatabase();
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
        PaymentViewModel.ReloadPayments();
    }

    private void removeButton_Click(object sender, RoutedEventArgs e)
    {
        var payment = paymentDetailsBorder.DataContext as Payment;
        PaymentViewModel.RemovePayment(payment);
    }

    private void saveNewPaymentButton_Click(object sender, RoutedEventArgs e)
    {
        PaymentViewModel.AddPayment();
    }
}
