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
        private Singletons.AutoLooping _looper;

        public SettingsService(Settings settings, AutoLooping looper)
        {
            _settings = settings;
            _looper = looper;
        }

        public Task AddPreset(Models.Preset preset)
        {
            return Task.Run(() =>
            {
                _settings.AddUpdatePreset(preset);
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

        public Task<IEnumerable<Models.ShortPreset>> GetShortPresets()
        {
            return Task.Run(() =>
            {
                return _settings.GetPresets().Select(pres => new Models.ShortPreset() { Id = pres.Id, Name = pres.Name });
            });
        }

        public Task<IEnumerable<Models.LoopObject>> GetLoops()
        {
            return Task.FromResult(_settings.GetLoops());
        }

        public Task AddUpdateLoop(Models.LoopObject lpObj)
        {
            return Task.Run(() =>
            {
                _settings.AddLoop(lpObj);
            });
        }

        public Task RemoveLoop(Models.LoopObject lpObj)
        {
            return Task.Run(() =>
            {
                _settings.RemoveLoop(lpObj);
            });
        }

        public Task<bool> IsLoopingActive()
        {
            return Task.FromResult(_looper.IsActive);
        }

        public Task SetLoopingActive(bool state)
        {
            return Task.Run(() =>
            {
                if (state)
                    _looper.StartLooping();
                else
                    _looper.EndLooping();
            });
        }

        public Task<Models.LoopInfo> GetLoopInfo()
        {
            return Task.Run(() =>
            {
                return new Models.LoopInfo()
                {
                    CurrentId = _looper.RunningLoopId,
                    TimeRemaing = _looper.TimeRemaining
                };
            });
        }

        public Task ResetLooping(Guid id)
        {
            return Task.Run(() =>
            {
                _looper.StartLoop(id);
            });
        }



    }
}
