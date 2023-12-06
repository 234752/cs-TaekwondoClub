using DB.Entities;

namespace MobileMAUI.Pages;

public partial class CostsPage : ContentPage
{
	public CostsPage()
	{
		InitializeComponent();
	}
    private void OnStartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (StartDatePicker.Date > EndDatePicker.Date) { EndDatePicker.Date = StartDatePicker.Date; }
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterCollections();
        }
    }
    private void OnEndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterCollections();
        }
    }
}