using DB.Entities;

namespace MobileMAUI.Pages;

public partial class UpcomingEventsPage : ContentPage
{
	public List<Event> Events { get; set; }
	public UpcomingEventsPage()
	{
		InitializeComponent();
	}
}