using DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.ViewModels;

public class EventViewModel : BaseViewModel
{
    public ObservableCollection<Event> Events { get; set; }
    public EventViewModel(MainWindow mainWindow, ObservableCollection<Event> events) : base(mainWindow)
    {
        Events = events;
    }
    public async Task SaveEventsToDatabase()
    {
        MainWindow.SaveChangesToDatabase();
    }

    public async Task ReloadEvents()
    {
       MainWindow.ReloadEvents();
    }

}
