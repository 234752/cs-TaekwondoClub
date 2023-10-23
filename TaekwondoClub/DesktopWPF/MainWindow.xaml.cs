using System;
using System.Collections.Generic;
using System.Configuration;
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
using DB;
using Microsoft.EntityFrameworkCore;

namespace DesktopWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["pogconnectionstring2"].ConnectionString);
        var dbContext = new DataContext(optionsBuilder.Options);
        dbContext.Customers.Load();
        var data = dbContext.Customers.Local.ToObservableCollection();
        this.DataContext = data;

        InitializeComponent();
    }
}
