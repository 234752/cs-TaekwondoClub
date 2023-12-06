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
            DataContext = DocumentsViewModel;
            generateDocumentButton.IsEnabled = false;
        }

        private void PickOutputFolderButton_Click(object sender, RoutedEventArgs e)
        {
            using (var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string selectedFolder = folderBrowserDialog.SelectedPath;
                    DocumentsViewModel.FolderPath = selectedFolder;
                    outputFolderLabel.Content = selectedFolder;
                    generateDocumentButton.IsEnabled = true;
                }
            }
        }
        private void GenerateDocumentButton_Click(object sender, RoutedEventArgs e)
        {
            var filename = DocumentsViewModel.Filename;
            var folder = DocumentsViewModel.FolderPath;
            if(string.IsNullOrWhiteSpace(filename)) 
            {
                MessageBox.Show($"Please enter the name of file");
                return;
            }
            string fullPath = System.IO.Path.Combine(folder, filename);
            if(documentTypeComboBox.SelectedIndex == 0)
            {
                ExcelFileManager.GenerateTimetable(fullPath, DocumentsViewModel.Events);

            } else if (documentTypeComboBox.SelectedIndex == 1)
            {
                ExcelFileManager.GenerateAttendanceList(fullPath, DocumentsViewModel.Events, DocumentsViewModel.Attendances);
            } else
            {
                ExcelFileManager.GenerateExpensesSummary(fullPath, DocumentsViewModel.Payments);
            }

            MessageBox.Show($$"""File saved in: {{folder}}, as "{{filename}}.xslx" """);
        }
    }
}