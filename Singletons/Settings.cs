using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Singletons
{
    public class Settings
    {
        public const string FileName = "settings.json";

        public class SaveObject
        {
            public List<Models.Preset> Presets = new List<Models.Preset>();
            public List<Models.LoopObject> Loops = new List<Models.LoopObject>();


        }

        private SaveObject _saveObject;

        public Settings()
        {
            _saveObject = new SaveObject();
            try
            {
                Load();
            }
            catch { }
        }

        public void AddUpdatePreset(Models.Preset preset)
        {
            var exPres = _saveObject.Presets.FirstOrDefault(prs => prs.Name.Equals(preset.Name));
            if (exPres != null)
            {
                _saveObject.Presets.Remove(exPres);
                preset.Id = exPres.Id;
            }
            _saveObject.Presets.Add(preset);
            Save();
        }
        public void RemovePreset(Models.Preset preset)
        {
            _saveObject.Presets.Remove(_saveObject.Presets.FirstOrDefault(pres => pres.Id.Equals(preset.Id)));
            Save();
        }

        public IEnumerable<Models.Preset> GetPresets()
        {
            return _saveObject.Presets;
        }
        public Models.Preset GetPresetById(Guid id)
        {
            return _saveObject.Presets.First(pres => pres.Id.Equals(id));
        }


        public void AddLoop(Models.LoopObject loop)
        {
            var exLoop = _saveObject.Loops.FirstOrDefault(prs => prs.Id.Equals(loop.Id));
            if (exLoop != null)
                _saveObject.Loops.Remove(exLoop);
            _saveObject.Loops.Add(loop);

            Save();
        }
        public void RemoveLoop(Models.LoopObject loop)
        {
            _saveObject.Loops.Remove(_saveObject.Loops.FirstOrDefault(pres => pres.Id.Equals(loop.Id)));
            Save();
        }

        public IEnumerable<Models.LoopObject> GetLoops()
        {
            return _saveObject.Loops;
        }

        private void Save()
        {
            var jsSave = Newtonsoft.Json.JsonConvert.SerializeObject(_saveObject);
            System.IO.File.WriteAllText(FileName, jsSave, System.Text.Encoding.UTF8);
        }

        private void Load()
        {
            var jsText = System.IO.File.ReadAllText(FileName, System.Text.Encoding.UTF8);

            _saveObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveObject>(jsText); 

        }

    }
}
