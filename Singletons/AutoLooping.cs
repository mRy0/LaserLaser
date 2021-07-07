using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Singletons
{
    public class AutoLooping
    {
        private System.Threading.Thread _loopThread;
        private Settings _settings;
        private OpenDMX _openDmx;

        public Guid RunningLoopId { set; get; } = Guid.Empty;
        public int TimeRemaining { set; get; }

        private volatile bool _threadRunning = false;

        public bool IsActive
        {
            get
            {
                return _threadRunning;
            }
        }

        private Guid _forceIndexId = Guid.NewGuid();
        private bool _forceIndex = false;

        public AutoLooping(Settings settings, OpenDMX openDmx)
        {
            _settings = settings;
            _openDmx = openDmx;
        }

        public void StartLooping()
        {
            if (_threadRunning)
                return;
            _threadRunning = true;
            _loopThread = new System.Threading.Thread(LoopThread);
            _loopThread.Start();
        }

        public void EndLooping()
        {
            _threadRunning = false;
            
        }

        public void StartLoop(Guid id)
        {
            _forceIndexId = id;
            _forceIndex = true;
        }


        private void LoopThread()
        {

            while (_threadRunning)
            {
                try
                {
                    Models.LoopObject runningLoop;

                    if (!_forceIndex)
                    {
                        var loops = _settings.GetLoops().Where(lp => lp.Active && !lp.PresetId.Equals(Guid.Empty)).OrderBy(loop => loop.Order).ToList();
                        if (loops.Count == 0)
                            continue;

                        var idx = loops.FindIndex(lp => lp.Id.Equals(RunningLoopId));

                        if (idx == -1 || idx >= loops.Count - 1)
                            idx = 0;
                        else
                            idx++;

                        runningLoop = loops[idx];
                    }
                    else
                    {
                        _forceIndex = false;
                        runningLoop =  _settings.GetLoops().First(lp => lp.Id.Equals(_forceIndexId));
                    }
                    RunningLoopId = runningLoop.Id;



                    var preset = _settings.GetPresetById(runningLoop.PresetId);
                    Console.WriteLine("starting preset: " + preset.Name);

                    foreach(var dmxSett in preset.Settings)
                    {
                        _openDmx.SetDMXValue(dmxSett.DMXAddress, dmxSett.Value);
                    }

                    var end = DateTime.Now.AddMilliseconds(runningLoop.Time);

                    while(end > DateTime.Now && _threadRunning && !_forceIndex)
                    {
                        TimeRemaining = (int) (end - DateTime.Now).TotalMilliseconds;
                        System.Threading.Thread.Sleep(10);
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    System.Threading.Thread.Sleep(10);
                }

            }
        }
    }
}
