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
using System.Linq;

namespace Metro_Navigation.Sources.View
{
    public partial class MetroControl : UserControl
    {

        #region Constructor

        public MetroControl()
        {
            InitializeComponent();
            names = new Dictionary<string, ushort>();
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
            border.Child = new Label()
            {
                Foreground = new SolidColorBrush(Colors.White),
                FontFamily = new FontFamily("Century Gothic")
            };
            PopupWindow.Child = border;
            BackgroundGrid.Children.Add(PopupWindow);

            AB = new ushort[2];
            train = new Train();
            Canvas.SetTop(train, 0);
            Canvas.SetLeft(train, 0);
            Canvas trainCanvas = new Canvas();
            BackgroundGrid.Children.Add(trainCanvas);
            trainCanvas.Children.Add(train);
        }

        #endregion

        #region Properties

        public static ushort[] AB { get; set; }
        private static Dictionary<string, ushort> names;

        private const double StationW = 25;
        private static Canvas stationsCanvas;
        private static Canvas connectionsCanvas;

        private static Popup PopupWindow;

        private static Dictionary<ushort, StationControl> stations;
        private static Dictionary<StationControl, ushort> ids;
        private static List<Line> connectionLines;

        private static Train train;

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
            get { return (ObservableCollection<Connection>)GetValue(ConnectionsListDependency); }
            set
            {
                SetValue(ConnectionsListDependency, value);
            }
        }

        public ObservableCollection<ushort> StationPath
        {
            get { return (ObservableCollection<ushort>)GetValue(PathDependency); }
            set
            {
                SetValue(PathDependency, value);
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
                        X1 = Canvas.GetLeft(stations[c.A]) + StationW / 2,
                        Y1 = Canvas.GetTop(stations[c.A]) + StationW / 2,
                        X2 = Canvas.GetLeft(stations[c.B]) + StationW / 2,
                        Y2 = Canvas.GetTop(stations[c.B]) + StationW / 2
                    };
                    //if connection is for pedestrians, it will be dashed on the map
                    if (c.Type == ConnectionType.Pedestrian)
                    {
                        line.StrokeDashArray = new DoubleCollection(new double[] { 1, 2 });
                        line.StrokeDashCap = PenLineCap.Round;
                    }
                    connectionsCanvas.Children.Add(line);
                    connectionLines.Add(line);
                }
            }
        }

        public static readonly DependencyProperty PathDependency =
            DependencyProperty.Register("StationPath", typeof(ObservableCollection<ushort>), typeof(MetroControl),
                new FrameworkPropertyMetadata(null, OnPathDependencyChanged));

        private static void OnPathDependencyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var n = e.NewValue as ObservableCollection<ushort>;
            var path = new List<Point>();
            foreach (var id in n)
            {
                Point p = new Point();
                var s = stations[id];
                p.X = Canvas.GetLeft(s) + s.Width / 2 - train.Width / 2;
                p.Y = Canvas.GetTop(s) + s.Height / 2 - train.Height / 2;
                path.Add(p);
            }
            train.PointsToPath = path;
            train.StartMoving();
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
            ids.Add(s, id);
            Canvas.SetLeft(s, w * xPosition);
            Canvas.SetTop(s, w * yPosition);
            stationsCanvas.Children.Add(s);

            names.Add(name, id);
        }


        //this methods closes pop-up border when user's cursors leaves station mark
        private static void S_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            PopupWindow.IsOpen = false;
        }

        //this methods opens pop-up border when user's cursor is on the station mark
        private static void S_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!PopupWindow.IsOpen && sender != null)
            {
                PopupWindow.IsOpen = true;
                StationControl s = sender as StationControl;
                var b = (Border)PopupWindow.Child;
                var l = (Label)b.Child;
                l.Content = s.StationName;
                var point = Mouse.GetPosition(Application.Current.MainWindow);
                PopupWindow.HorizontalOffset = Canvas.GetLeft(s);
                PopupWindow.VerticalOffset = Canvas.GetTop(s) - 30;
            }
        }


        public static void SetA(string stationName)
        {
            if (AB[0] != 0)
            {
                stations[AB[0]].EndAnimation();
            }
            AB[0] = names[stationName];
            stations[AB[0]].StartAnimation();
        }

        public static void SetB(string stationName)
        {
            if (AB[1] != 0)
            {
                stations[AB[1]].EndAnimation();
            }
            AB[1] = names[stationName];
            stations[AB[1]].StartAnimation();
        }

        #endregion
    }
}
