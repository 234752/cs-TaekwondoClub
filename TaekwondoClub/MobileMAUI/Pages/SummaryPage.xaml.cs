namespace MobileMAUI.Pages;

public partial class SummaryPage : ContentPage
{
    public RestService RestService { get; set; }
    public MainViewModel ViewModel { get; set; }

    public SummaryPage()
    {
        ViewModel = BindingContext as MainViewModel;
        InitializeComponent();
    }
    private void OnStartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if(StartDatePicker.Date > EndDatePicker.Date) { EndDatePicker.Date = StartDatePicker.Date; }
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterEventsByDate();
        }
    }
    private void OnEndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        EndDatePicker.MinimumDate = StartDatePicker.Date;
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterEventsByDate();
        }
    }
}