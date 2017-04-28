using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace Metro_Navigation.Sources.Model
{
    class Metro
    {

        #region Constructor
        public Metro()
        {
            
        }
        #endregion

        #region Properties

        private string namesSrc;
        public string NamesSrc
        {
            get { return namesSrc; }
            set {
                if (namesSrc != value)
                {
                    namesSrc = value;
                    stations = new List<Station>();
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

        private List<Station> stations;
        private Dictionary<ushort, ushort> connections;
        private GraphAdjList graph;

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
