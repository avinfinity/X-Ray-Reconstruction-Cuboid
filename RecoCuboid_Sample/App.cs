using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecoCuboid_Sample
{
    class App : Application
    {
        [STAThread]
        static void Main(string[]args)
        {
            App app = new App();
            app.Resources.Source = new Uri(@"../../Properties/Resources.xaml", UriKind.Relative);
            MainWindow window = new MainWindow();
            window.DataContext = new MainViewModel();
            app.Run(window);
        }
    }
}
