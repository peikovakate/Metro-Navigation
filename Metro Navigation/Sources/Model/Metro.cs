using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

        private Dictionary<ushort, ushort> connections;
        private GraphAdjList graph;

        private string namesSrc;
        public string NamesSrc
        {
            get { return namesSrc; }
            set {
                if (namesSrc != value)
                {
                    namesSrc = value;
                    stations = new ObservableCollection<Station>();  
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
                    connections = new Dictionary<ushort, ushort>();
                }
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
        #endregion

        #region Methods

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
                        Line = Convert.ToInt32(fields[2]),
                        XPosition = Convert.ToDouble(fields[3]),
                        YPosition = Convert.ToDouble(fields[4])
                    };
                    stations.Add(s);
                }
            }

            graph = new GraphAdjList(stations.Count);

            using (TextFieldParser parser = new TextFieldParser(connectionsSrc))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();        
                    //connections.Add(Convert.ToUInt16(fields[0]), Convert.ToUInt16(fields[1]));
                    graph.AddEdge(Convert.ToUInt16(fields[0]), Convert.ToUInt16(fields[1]));
                }
            }
        }

        public void GO(ushort a, ushort b)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch(graph);
            var path = bfs.BFS(7, 21);
            
        }
        #endregion
    }
}
