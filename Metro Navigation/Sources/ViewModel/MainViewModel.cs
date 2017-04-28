using System;
using Metro_Navigation.Sources.Model;


namespace Metro_Navigation.Sources.ViewModel
{
    class MainViewModel
    {
        private const string CONNECTIONS_PATH = "data/connections.csv";
        private const string STATIONS_PATH = "data/stations.csv";

        public MainViewModel()
        {
            Metro metro = new Metro();

            string path = AppDomain.CurrentDomain.BaseDirectory;

            metro.ConnectionsSrc = path + CONNECTIONS_PATH;
            metro.NamesSrc = path + STATIONS_PATH;
            metro.LoadData();
            metro.GO(7, 21);
        }
    }
}
