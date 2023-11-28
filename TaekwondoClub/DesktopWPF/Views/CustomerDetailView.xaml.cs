using DB.Entities;
using DesktopWPF.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for CustomerDetailView.xaml
    /// </summary>
    public partial class CustomerDetailView : Window
    {
        public Customer Customer { get; set; }
        public bool SaveChanges { get; set; } = false;

        public CustomerDetailView(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            DataContext = Customer;
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
    }
}
