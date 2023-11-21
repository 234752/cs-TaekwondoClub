﻿using DB.Entities;
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

    private async void saveButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        button.IsEnabled = false;
        await PaymentViewModel.SavePaymentsToDatabase();
        button.IsEnabled = true;
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
    private void MonthYearTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        var textbox = sender as TextBox;
        if (!RegexValidator.IsMonthYearPattern(textbox.Text + e.Text))
        {
            e.Handled = true;
        }
    }
}
