using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Models
{
    public class LoopObject
    {
        public int Order { set; get; }
        public Guid PresetId { set; get; }
        public int Time { set; get; }

    }
}
