using LaserWebServer.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaserWebServer.Data
{
    public class OpenDMXService
    {
        private Singletons.OpenDMX _openDMXService;

        public OpenDMXService(OpenDMX openDMXService)
        {
            _openDMXService = openDMXService;
        }

        public Task<Models.FTD2Device[]> GetDevicesAsync()
        {
            return Task.Run(() =>
            {
                return _openDMXService.GetDevices().Select(ftd =>
                {
                    return new Models.FTD2Device()
                    {
                        LocId = ftd.LocId,
                        Description = $"{ftd.Description} ({ftd.Type}) - Port: {ftd.SerialNumber}"
                    };
                }).ToArray();
            });
        }

        public Task<string> GetConnectedDevice()
        {
            return Task.Run(() =>
            {
                return _openDMXService.GetConnectedDeviceInfo();
            });
        }

        public Task OpenDeviceAsync(uint locId)
        {
            return Task.Run(() =>
            {
                _openDMXService.Open(locId);
            });
        }

        public Task CloseDeviceAsync()
        {
            return Task.Run(() =>
            {
                _openDMXService.Close();
            });
        }


        public Task<byte> GetChannelValue(uint channel)
        {
            return Task.Run(() =>
            {
                return _openDMXService.GetDMXValue(channel);
            });
        }

        public Task SetChannelValue(uint channel, byte value)
        {
            return Task.Run(() =>
            {
                _openDMXService.SetDMXValue(channel, value);
            });
        }
    }
}
