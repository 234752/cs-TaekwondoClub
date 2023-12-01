using DesktopWPF.Excel;
using DesktopWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


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
            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    
                    ExcelFileManager.WriteMessage(selectedFolder, "testWorkBook");
                    MessageBox.Show($"File saved in: {selectedFolder}");
                }
            }
            
        }
    }
}
