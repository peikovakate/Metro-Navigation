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
using Metro_Navigation.Sources.Model;
using System.Reflection;

namespace Metro_Navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Metro metro = new Metro();

            string path = AppDomain.CurrentDomain.BaseDirectory;



            metro.ConnectionsSrc = "data/connections.csv";
            metro.NamesSrc = path+ "data/stations.csv";
            metro.LoadData();
            metro.GO(7, 21);
        }
    }
}
