using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using System.Collections.Specialized;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Metro_Navigation.Sources.View
{
    public partial class MetroControl : UserControl
    {

        #region Constructor

        private const double StationW = 20;

        public MetroControl()
        {
            InitializeComponent();
            stations = new Dictionary<ushort, StationControl>();
            connectionLines = new List<Line>();

            connectionsCanvas = new Canvas();
            BackgroundGrid.Children.Add(connectionsCanvas);

            stationsCanvas = new Canvas();
            BackgroundGrid.Children.Add(stationsCanvas);
        }

        #endregion

        #region Properties
        private static Canvas stationsCanvas;
        private static Canvas connectionsCanvas;

        private static Dictionary<ushort, StationControl> stations;
        private static List<Line> connectionLines;

        public ObservableCollection<Station> StationsList
        {
            get { return (ObservableCollection<Station>)GetValue(StationsListDependency); }
            set
            {
                SetValue(StationsListDependency, value);
            }
        }
        
        public ObservableCollection<Connection> ConnectionsList
        {
            get { return (ObservableCollection<Connection>)GetValue(StationsListDependency); }
            set
            {
                SetValue(StationsListDependency, value);
            }
        }

        #endregion

        #region Dependecies implementation
        public static readonly DependencyProperty StationsListDependency =
            DependencyProperty.Register("StationsList", typeof(ObservableCollection<Station>), typeof(MetroControl),
                new FrameworkPropertyMetadata(null, OnStationsListDependencyChanged));

        private static void OnStationsListDependencyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var n = e.NewValue as ObservableCollection<Station>;
            double w = (sender as MetroControl).ActualWidth;
            if (n != null)
            {
                foreach (var item in n)
                {
                    AddStation(item.Id, item.Name, item.XPosition, item.YPosition, item.LineColor, w);
                }
            }
        }

        public static readonly DependencyProperty ConnectionsListDependency =
            DependencyProperty.Register("ConnectionsList", typeof(ObservableCollection<Connection>), typeof(MetroControl),
        new FrameworkPropertyMetadata(null, OnConnectionsListDependencyChanged));

        private static void OnConnectionsListDependencyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var n = e.NewValue as ObservableCollection<Connection>;

            if (n != null)
            {
                foreach (var c in n)
                {
                    Line line = new Line()
                    {
                        StrokeThickness = 3,
                        Stroke = new SolidColorBrush(c.ConnectionColor),
                        X1 = Canvas.GetLeft(stations[c.A])+ StationW/2,
                        Y1 = Canvas.GetTop(stations[c.A])+ StationW / 2,
                        X2 = Canvas.GetLeft(stations[c.B])+ StationW / 2,
                        Y2 = Canvas.GetTop(stations[c.B])+ StationW / 2
                    };
                    connectionsCanvas.Children.Add(line);
                    connectionLines.Add(line);
                }
            }
        }

        #endregion

        #region Methods
        private static void AddStation(ushort id, string name, double xPosition, double yPosition, Color color, double w)
        {
            StationControl s = new StationControl()
            {
                StationName = name,
                StationColor = new SolidColorBrush(color),
                Width = StationW,
                Height = StationW
            };
            s.MouseEnter += S_MouseEnter;
            s.MouseLeave += S_MouseLeave;
            stations.Add(id, s);
            Canvas.SetLeft(s, w * xPosition);
            Canvas.SetTop(s, w * yPosition);
            stationsCanvas.Children.Add(s);
        }

        private static void S_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private static void S_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Console.WriteLine(((StationControl)(sender)).StationName);
        }

        public void AddConnection()
        {

        }
        #endregion
    }
}
