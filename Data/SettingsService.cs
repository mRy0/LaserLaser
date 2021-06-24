using LaserWebServer.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Data
{
    public class SettingsService
    {
        private Singletons.Settings _settings;

        public SettingsService(Settings settings)
        {
            _settings = settings;
        }

        public Task AddPreset(Models.Preset preset)
        {
            return Task.Run(() =>
            {
                _settings.AddPreset(preset);
            });
        }
        
        public Task RemovePreset(Models.Preset preset)
        {
            return Task.Run(() =>
            {
                _settings.RemovePreset(preset);
            });
        }


        public Task<IEnumerable<Models.Preset>> GetPresets()
        {
            return Task.Run(() =>
            {
                return _settings.GetPresets();
            });
        }




    }
}
