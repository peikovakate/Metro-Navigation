using System;
using Metro_Navigation.Sources.Model;
using System.Windows.Input;

namespace Metro_Navigation.Sources.ViewModel
{
    class MainViewModel
    {
        private const string CONNECTIONS_PATH = "data/connections.csv";
        private const string STATIONS_PATH = "data/stations.csv";
        private const string LINES_PATH = "data/lines.csv";

        public Metro MetroNavig { get; private set; }

        public ICommand Navigate { get; set; }

        public MainViewModel()
        {
            MetroNavig = new Metro();
            Navigate = new Command(arg => PassStationsToMetro(arg));

            string path = AppDomain.CurrentDomain.BaseDirectory;

            MetroNavig.ConnectionsSrc = path + CONNECTIONS_PATH;
            MetroNavig.NamesSrc = path + STATIONS_PATH;
            MetroNavig.LinesSrc = path + LINES_PATH;
            MetroNavig.LoadData();

        }

        private void PassStationsToMetro(object a)
        {
            ushort[] ab = (ushort[])a;
            MetroNavig.GO(ab[0], ab[1]);
        }
    }
}
