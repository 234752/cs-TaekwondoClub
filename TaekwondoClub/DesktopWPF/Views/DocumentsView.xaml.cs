using DesktopWPF.Excel;
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

namespace DesktopWPF.Views
{
    /// <summary>
    /// Interaction logic for DocumentsView.xaml
    /// </summary>
    public partial class DocumentsView : Page
    {
        public DocumentsViewModel DocumentsViewModel { get; set; }
        public DocumentsView(DocumentsViewModel documentsViewModel)
        {
            DocumentsViewModel = documentsViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExcelFileManager.WriteMessage("templateWorkbook", "hello world");
        }
    }
}
