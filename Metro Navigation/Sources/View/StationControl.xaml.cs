using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Metro_Navigation.Sources.View
{
    /// <summary>
    /// Interaction logic for StationControl.xaml
    /// </summary>
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


    }
}
