using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Metro_Navigation.Sources.Model;
using System.Reflection;

namespace Metro_Navigation.Sources.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CONNECTIONS_PATH = "data/connections.csv";
        private const string STATIONS_PATH = "data/stations.csv";

        public MainWindow()
        {
            InitializeComponent();
            Metro metro = new Metro();

            string path = AppDomain.CurrentDomain.BaseDirectory;

            metro.ConnectionsSrc = path + CONNECTIONS_PATH;
            metro.NamesSrc = path + STATIONS_PATH;
            metro.LoadData();
            metro.GO(7, 21);
        }
    }
}
