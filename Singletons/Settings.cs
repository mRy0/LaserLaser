using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Singletons
{
    public class Settings
    {
        private List<Models.Preset> _presets = new List<Models.Preset>();
        public void AddPreset(Models.Preset preset)
        {
            _presets.Add(preset);
        }
        public void RemovePreset(Models.Preset preset)
        {
            _presets.Remove(_presets.First(pres => pres.Id.Equals(preset.Id)));
        }

        public IEnumerable<Models.Preset> GetPresets()
        {
            return _presets;
        }
        public Models.Preset GetPresetById(Guid id)
        {
            return _presets.First(pres => pres.Id.Equals(id));
        }

    }
}
