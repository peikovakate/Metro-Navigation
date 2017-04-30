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
    /// Interaction logic for ConnectionControl.xaml
    /// </summary>
    public partial class ConnectionControl : UserControl
    {
        public ConnectionControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StationColorDependency =
            DependencyProperty.Register("ConnectionColor", typeof(Brush), typeof(ConnectionControl));

        public static readonly DependencyProperty LengthDependency =
            DependencyProperty.Register("Length", typeof(double), typeof(ConnectionControl));

        public Brush ConnectionColor
        {
            get { return (Brush)GetValue(StationColorDependency); }
            set { SetValue(StationColorDependency, value); }
        }

        public double Length
        {
            get { return (double)GetValue(StationColorDependency); }
            set { SetValue(StationColorDependency, value); }
        }
    }
}
