using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Metro_Navigation.Sources.View
{
    public partial class StationControl : UserControl
    {
        public static readonly DependencyProperty StationNameDependency =
            DependencyProperty.Register("StationName", typeof(string), typeof(StationControl));

        public static readonly DependencyProperty StationColorDependency =
            DependencyProperty.Register("StationColor", typeof(Brush), typeof(StationControl));

        public string StationName
        {
            get { return (string)GetValue(StationNameDependency); }
            set { SetValue(StationNameDependency, value); }
        }

        public Brush StationColor
        {
            get { return (Brush)GetValue(StationColorDependency); }
            set { SetValue(StationColorDependency, value); }
        }

        public StationControl()
        {
            InitializeComponent();
        }

        public void StartAnimation()
        {
            Storyboard s = (Storyboard)TryFindResource("Animate");
            s.Begin();
        }

        public void EndAnimation()
        {
            Storyboard s = (Storyboard)TryFindResource("Animate");
            s.Stop();
        }
    }
}
