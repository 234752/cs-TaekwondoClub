namespace MobileMAUI.Pages;

public partial class PaymentsPage : ContentPage
{
	public PaymentsPage()
	{
		InitializeComponent();
	}
    private void OnStartMonthYearPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (StartMonthYearPicker.Date > EndMonthYearPicker.Date) { EndMonthYearPicker.Date = StartMonthYearPicker.Date; }
        EndMonthYearPicker.MinimumDate = StartMonthYearPicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterPaymentsByMonthYear();
        }
    }
    private void OnEndMonthYearPicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndMonthYearPicker.MinimumDate = StartMonthYearPicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterPaymentsByMonthYear();
        }
    }
    private async void OnNotifyDuePaymentsButton_Clicked(object sender, EventArgs args)
    {
        var button = sender as Button;
        if (BindingContext is MainViewModel viewModel)
        {
            button.IsEnabled = false;
            await viewModel.SendEmailToDuePayments();
            button.IsEnabled = true;

        }
    }
}