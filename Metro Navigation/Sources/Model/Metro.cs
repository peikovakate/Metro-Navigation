using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Metro_Navigation.Sources.Model
{
    class Metro: INotifyPropertyChanged
    {

        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Constructor
        public Metro()
        {
            
        }


        #endregion

        #region Properties

        private GraphAdjList graph;
        private BreadthFirstSearch bfs;
        private Dictionary<int, Color> linesColors;
        private Dictionary<ushort, Station> stationsById;

        private string namesSrc;
        public string NamesSrc
        {
            get { return namesSrc; }
            set {
                if (namesSrc != value)
                {
                    namesSrc = value;
                    stations = new ObservableCollection<Station>();
                    stationsById = new Dictionary<ushort, Station>();
                    names = new ObservableCollection<string>();
                }
            }
        }

        private string connectionsSrc;
        public string ConnectionsSrc {
            get { return connectionsSrc; }
            set {
                if (connectionsSrc != value)
                {
                    connectionsSrc = value;
                    connections = new ObservableCollection<Connection>();
                }
            }
        }

        private string linesSrc;
        public string LinesSrc
        {
            get { return linesSrc; }
            set
            {
                linesSrc = value;
                linesColors = new Dictionary<int, Color>();
            }
        }

        private ObservableCollection<Station> stations;
        public ObservableCollection<Station> Stations
        {
            get { return stations; }
            set
            {
                stations = value;
                OnPropertyChanged("Stations");
            }
        }

        private ObservableCollection<Connection> connections;
        public ObservableCollection<Connection> Connections
        {
            get { return connections; }
            set
            {
                connections = value;
                OnPropertyChanged("Connections");
            }
        }

        private ObservableCollection<string> names;
        public ObservableCollection<string> Names
        {
            get { return names; }
            set
            {
                names = value;
                OnPropertyChanged("Names");
            }
        }

        private ObservableCollection<ushort> path;
        public ObservableCollection<ushort> Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        #endregion

        #region Public methods

        public void LoadData()
        {
            if (namesSrc == null)
            {
                throw new System.ArgumentException("NameSrc is uninitialized");
            }
            if (connectionsSrc == null)
            {
                throw new System.ArgumentException("ConnectionsSrc is uninitialized");
            }
            if(linesSrc == null)
            {
                throw new System.ArgumentException("LineSrc is uninitialized");
            }

            loadLinesData();
            loadStationsData();
            graph = new GraphAdjList(stations.Count);

            loadConnectionsData();
            bfs = new BreadthFirstSearch(graph);
        }

        public void GO(ushort a, ushort b)
        {
            Path = new ObservableCollection<ushort>(bfs.BFS(a, b));
        }
        #endregion

        #region Private Methods
        private void loadLinesData()
        {
            using (TextFieldParser parser = new TextFieldParser(linesSrc))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    linesColors.Add(Convert.ToInt32(fields[0]),
                        Color.FromArgb(255, Convert.ToByte(fields[1]), Convert.ToByte(fields[2]), Convert.ToByte(fields[3])));
                        
                }
            }
        }

        private void loadStationsData()
        {
            using (TextFieldParser parser = new TextFieldParser(namesSrc))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    Station s = new Station()
                    {
                        Id = Convert.ToUInt16(fields[0]),
                        Name = fields[1],
                        LineColor = linesColors[Convert.ToInt32(fields[2])],
                        XPosition = Convert.ToDouble(fields[3]),
                        YPosition = Convert.ToDouble(fields[4])
                    };
                    stations.Add(s);
                    names.Add(s.Name);
                    stationsById.Add(s.Id, s);
                }
            }
        }

        private void loadConnectionsData()
        {
            using (TextFieldParser parser = new TextFieldParser(connectionsSrc))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    Connection c = new Connection()
                    {
                        A = Convert.ToUInt16(fields[0]),
                        B = Convert.ToUInt16(fields[1]),
                        Type = fields[2] == "t" ? ConnectionType.Train : ConnectionType.Pedestrian,
                    };
                    if(stationsById[c.A].LineColor == stationsById[c.B].LineColor)
                    {
                        c.ConnectionColor = stationsById[c.A].LineColor;
                    }else
                    {
                        c.ConnectionColor = Colors.Gray;
                    }
                    connections.Add(c);
                    graph.AddEdge(c.A, c.B);
                }
            }
        }
        #endregion
    }
}
