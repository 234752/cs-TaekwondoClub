namespace MobileMAUI;

public partial class AppShell : Shell
{
    public RestService RestService { get; set; }
    public AppShell()
    {        
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        RestService = new RestService();
        await RestService.FetchDataContext();
        var binding = new MainViewModel(RestService);
        BindingContext = binding;
    }
}