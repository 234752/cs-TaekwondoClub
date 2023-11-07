namespace MobileMAUI;

public partial class AppShell : Shell
{
    public RestService RestService { get; set; }
    public AppShell()
    {
        RestService = new RestService();
        InitializeComponent();
    }
}