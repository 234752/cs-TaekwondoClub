using DB.Entities;
using DesktopWPF.Validators;
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
using System.Windows.Shapes;

namespace DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for PaymentDetailView.xaml
    /// </summary>
    public partial class PaymentDetailView : Window
    {
        private MinimalViewModel model;
        public bool SaveChanges { get; set; } = false;
        
        private class MinimalViewModel()
        {
            public bool IsItClubsExpense { get; set; }
            public Payment Payment { get; set; }
            public List<Customer> Customers { get; set; }
        }

        public PaymentDetailView(Payment payment, List<Customer> customers)
        {
            InitializeComponent();
            var clubExpense = payment.Customer is null;
            model = new() { Payment = payment, Customers = customers, IsItClubsExpense = clubExpense };
            DataContext = model;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges = true;
            Close();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!RegexValidator.IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void ToggleButton_CheckedUnchecked(object sender, RoutedEventArgs e)
        {
            if(model.IsItClubsExpense) 
            {
                model.Payment.Customer = null;
                model.Payment.CustomerId = null;
                customerSelectionComboBox.SelectedItem = null;
                customerSelectionTextBlock.Visibility = Visibility.Collapsed;
                customerSelectionComboBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                customerSelectionComboBox.SelectedItem = model.Customers.FirstOrDefault();
                customerSelectionTextBlock.Visibility = Visibility.Visible;
                customerSelectionComboBox.Visibility = Visibility.Visible;
            }
        }
    }
}
