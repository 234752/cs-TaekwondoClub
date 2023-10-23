using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWPF.ViewModels;

public abstract class BaseViewModel
{
    protected MainWindow MainWindow;
    public BaseViewModel(MainWindow mainWindow) { MainWindow = mainWindow; }

}
