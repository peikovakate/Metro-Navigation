using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro_Navigation.Sources
{
    public class Station
    {
        public string Name { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public Color LineColor { get; set; }
        public ushort Id { get; set; }
    }
}
