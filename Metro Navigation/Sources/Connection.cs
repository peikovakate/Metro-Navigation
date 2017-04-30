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
        public ushort A { get; set; }
        public ushort B { get; set; }
        public Color ConnectionColor {get; set;}
        public ConnectionType Type { get; set; }
    }
}
