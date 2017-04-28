using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro_Navigation.Sources.Model
{
    class Station
    {
        public string Name { get; set; }
        float XPosition { get; set; }
        float YPosition { get; set; }
        int Line { get; set; }
    }
}
