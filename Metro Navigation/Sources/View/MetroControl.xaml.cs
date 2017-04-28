using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Specialized;
using System;

namespace Metro_Navigation.Sources.View
{
    /// <summary>
    /// Interaction logic for MetroControl.xaml
    /// </summary>
    public partial class MetroControl : UserControl
    {

        #region Constructor

        public MetroControl()
        {
            InitializeComponent();
            stations = new Dictionary<ushort, StationControl>();
            metroCanvas = new Canvas();
            metroCanvas.Width = 600;
            metroCanvas.Height = 600;
            BackgroundGrid.Children.Add(metroCanvas);
        }

        #endregion

        #region Properties
        private static Dictionary<ushort, StationControl> stations;
        private static Canvas metroCanvas;
        public ObservableCollection<Station> StationsList
        {
            get { return (ObservableCollection<Station>)GetValue(StationsListDependency); }
            set
            {
                SetValue(StationsListDependency, value);
            }
        }

        #endregion

        #region Dependecy implementation
        public static readonly DependencyProperty StationsListDependency =
            DependencyProperty.Register("StationsList", typeof(ObservableCollection<Station>), typeof(MetroControl),
                new FrameworkPropertyMetadata(null, OnStationsListDependencyChanged));

        private static void OnStationsListDependencyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var n = e.NewValue as ObservableCollection<Station>;

            if (n != null)
            {
                foreach (var item in n)
                {
                    AddStation(item.Id, item.Name, item.XPosition, item.YPosition);
                }
            }
        }

        #endregion

        #region Methods
        private static void AddStation(ushort id, string name, double xPosition, double yPosition)
        {
            StationControl s = new StationControl()
            {
                StationName = name,
                StationColor = new SolidColorBrush(Colors.Red),
                Width = 20,
                Height = 20
            };
            s.MouseEnter += S_MouseEnter;
            s.MouseLeave += S_MouseLeave;
            stations.Add(id, s);
            Canvas.SetLeft(s, metroCanvas.Width * xPosition);
            Canvas.SetTop(s, metroCanvas.Height * yPosition);
            metroCanvas.Children.Add(s);
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
