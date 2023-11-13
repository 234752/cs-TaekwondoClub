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
    private void OnDaysPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (BindingContext is MainViewModel viewModel)
        {
            viewModel.FilterEventsByDate();
        }
    }
}