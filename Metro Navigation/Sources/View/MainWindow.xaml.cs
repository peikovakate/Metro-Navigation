using System.Windows;
using System.Windows.Controls;

namespace Metro_Navigation.Sources.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = sender as ComboBox;
            if (c == ComboBoxA)
            {
                MetroControl.SetA((string)c.SelectedValue);
                
            }else if (c == ComboBoxB)
            {
                MetroControl.SetB((string)c.SelectedValue);
            }
        }
    }
}
