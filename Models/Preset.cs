using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Models
{
    public class Preset
    {
        public class PresetSetting
        {
            public uint DMXAddress { set; get; }
            public byte Value { set; get; }
        }

        public Guid Id { set; get; }
        public string Name { set; get; }

        public List<PresetSetting> Settings { set; get; }

    }
}
