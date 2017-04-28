using System.Windows;
using Metro_Navigation.Sources.View;
using Metro_Navigation.Sources.ViewModel;

namespace Metro_Navigation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        var mw = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        mw.Show();
        }

    }
}
