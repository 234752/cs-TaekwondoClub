using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Entities;

namespace MobileMAUI;

public class BindingContext
{
    public ObservableCollection<Event> Events { get; set; }
    public BindingContext(RestService restService)
    {
        Events = new ObservableCollection<Event>(restService.Events);
    }
}
