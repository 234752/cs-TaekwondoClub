using DB.Entities;

namespace MobileMAUI.Pages;

public partial class UpcomingEventsPage : ContentPage
{
	public UpcomingEventsPage()
	{
		InitializeComponent();
	}
    private void OnStartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (StartDatePicker.Date > EndDatePicker.Date) { EndDatePicker.Date = StartDatePicker.Date; }
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterEvents();
        }
    }
    private void OnEndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterEvents();
        }
    }
    private async void OnNotifyEventParticipantsButton_Clicked(object sender, EventArgs args)
    {
        var button = sender as Button;
        if (BindingContext is MainViewModel viewModel)
        {
            button.IsEnabled = false;
            await viewModel.SendEmailToEventParticipants();
            button.IsEnabled = true;

        }
    }
}