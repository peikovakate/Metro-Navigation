using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using System.Collections.Specialized;
using System;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Metro_Navigation.Sources.View
{
    public partial class MetroControl : UserControl
    {

        #region Constructor

        public MetroControl()
        {
            InitializeComponent();
            stations = new Dictionary<ushort, StationControl>();
            ids = new Dictionary<StationControl, ushort>();
            connectionLines = new List<Line>();

            //this canvas will contain station marks (StationControls)
            connectionsCanvas = new Canvas();
            BackgroundGrid.Children.Add(connectionsCanvas);

            //this canvas will contain connections between station marks
            stationsCanvas = new Canvas();
            BackgroundGrid.Children.Add(stationsCanvas);

            //pop-up with name of the station
            PopupWindow = new Popup();
            PopupWindow.Placement = PlacementMode.Relative;
            PopupWindow.IsOpen = false;
            Border border = new Border();
            border.Background = new SolidColorBrush(Colors.Gray);
            border.Child = new Label() { Foreground = new SolidColorBrush(Colors.White) };
            PopupWindow.Child = border;
            BackgroundGrid.Children.Add(PopupWindow);

            
        }

        #endregion

        #region Properties

        private static StationControl stationA;
        private static StationControl stationB;

        public static ushort[] AB { get; set; }
 

        private const double StationW = 25;
        private static Canvas stationsCanvas;
        private static Canvas connectionsCanvas;

        private static Popup PopupWindow;

        private static Dictionary<ushort, StationControl> stations;
        private static Dictionary<StationControl, ushort> ids;
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
                    //adding connections to map
                    Line line = new Line()
                    {
                        StrokeThickness = 3,
                        Stroke = new SolidColorBrush(c.ConnectionColor),
                        X1 = Canvas.GetLeft(stations[c.A])+ StationW/2,
                        Y1 = Canvas.GetTop(stations[c.A])+ StationW / 2,
                        X2 = Canvas.GetLeft(stations[c.B])+ StationW / 2,
                        Y2 = Canvas.GetTop(stations[c.B])+ StationW / 2
                    };
                    //if connection is for pedestrians, it will be dashed on the map
                    if(c.Type == ConnectionType.Pedestrian)
                    {
                        line.StrokeDashArray = new DoubleCollection(new double[] {1, 2});
                        line.StrokeDashCap = PenLineCap.Round;
                    }
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
            s.MouseDown += S_MouseDown;

            stations.Add(id, s);
            ids.Add(s, id);
            Canvas.SetLeft(s, w * xPosition);
            Canvas.SetTop(s, w * yPosition);
            stationsCanvas.Children.Add(s);
        }

        private static void S_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StationControl s = sender as StationControl;
            if(stationA == null)
            {
                stationA = s;
                s.StartAnimation();
            }
            else if(stationA!= s && stationB == null)
            {
                stationB = s;
                AB = new ushort[] { ids[stationA], ids[stationB] };
                s.StartAnimation();
            }
            
        }

        //this methods closes pop-up border when user's cursors leaves station mark
        private static void S_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupWindow.IsOpen = false;
        }

        //this methods opens pop-up border when user's cursor is on the station mark
        private static void S_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!PopupWindow.IsOpen && sender!=null)
            {
                PopupWindow.IsOpen = true;
                StationControl s = sender as StationControl;
                var b = (Border)PopupWindow.Child;
                var l = (Label)b.Child;
                l.Content = s.StationName;
                var point = Mouse.GetPosition(Application.Current.MainWindow);
                PopupWindow.HorizontalOffset = Canvas.GetLeft(s);
                PopupWindow.VerticalOffset = Canvas.GetTop(s)-30;
            }
        }

        #endregion
    }
}
