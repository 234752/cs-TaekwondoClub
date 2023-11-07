using MobileMAUI.Pages;

namespace MobileMAUI;

public partial class MainPage : ContentPage
{
    public RestService RestService { get; set; }

    public MainPage()
    {
        RestService = new RestService();
        RestService.FetchDataContext();
        
        InitializeComponent();
    }
}