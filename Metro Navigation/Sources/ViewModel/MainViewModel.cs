using System;
using Metro_Navigation.Sources.Model;


namespace Metro_Navigation.Sources.ViewModel
{
    class MainViewModel
    {
        private const string CONNECTIONS_PATH = "data/connections.csv";
        private const string STATIONS_PATH = "data/stations.csv";
        private const string LINES_PATH = "data/lines.csv";

        public Metro MetroNavig { get; private set; }

        public MainViewModel()
        {

            MetroNavig = new Metro();

            string path = AppDomain.CurrentDomain.BaseDirectory;

            MetroNavig.ConnectionsSrc = path + CONNECTIONS_PATH;
            MetroNavig.NamesSrc = path + STATIONS_PATH;
            MetroNavig.LinesSrc = path + LINES_PATH;
            MetroNavig.LoadData();

            //foreach (var item in metro.stations)
            //{
            //    MetroC.AddStation(item.Id, item.Name, item.XPosition, item.YPosition);
            //}



        }
    }
}
