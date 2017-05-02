using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Metro_Navigation.Sources
{
    public enum ConnectionType { Train, Pedestrian }

    public class Connection
    {
        public ushort A { get; set; } //station id
        public ushort B { get; set; } //station id
        public Color ConnectionColor { get; set; } //in most cases the color of line
        public ConnectionType Type { get; set; } //can be train or pedestrian
    }
}
