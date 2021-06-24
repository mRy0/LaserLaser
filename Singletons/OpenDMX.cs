using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;

namespace LaserWebServer.Singletons
{
    public class OpenDMX
    {
        private const int DMX_CHANNEL = 513;
        private const byte DEV_DATA_BITS = 8;
        private const uint DEV_BAUDRATE = 250000; //  3M/12
        private const byte DEV_STOP_BITS = 2;
        private const byte DEV_PARITY = 0;
        private const byte DEV_FLOW = 0;
        private const byte DEV_PURGE_RX = 1;
        private const byte DEV_PURGE_TX = 2;

        private const int DMX_SLEEP = 25;

        private FTD2XX_NET.FTDI _device = null;


        private byte[] _dmxBuffer = new byte[DMX_CHANNEL];

        private volatile bool _writeThreadStop = false;
        private System.Threading.Thread _writeThread;

        public IEnumerable<FTDI.FT_DEVICE_INFO_NODE> GetDevices()
        {
            uint devCount = 0;
            _device.GetNumberOfDevices(ref devCount);

            var devices = new FTDI.FT_DEVICE_INFO_NODE[devCount];

            _device.GetDeviceList(devices);

            return devices;
        }

        public OpenDMX()
        {
            _device = new FTDI();
        }

        public void SetDMXValue(uint channel, byte value)
        {
            if (channel > _dmxBuffer.Length) throw new ArgumentException("max channel is: " + _dmxBuffer.Length);
            _dmxBuffer[channel] = value;
            Console.WriteLine("setting channel: " + channel + " to: " + value);
        }
        public byte GetDMXValue(uint channel)
        {
            if (channel > _dmxBuffer.Length) throw new ArgumentException("max channel is: " + _dmxBuffer.Length);
            return _dmxBuffer[channel];
        }

        public void Open(FTDI.FT_DEVICE_INFO_NODE device)
        {
            Open(device.LocId);
        }
        public void Open(uint locId)
        {
            _device.OpenByLocation(locId);
            _writeThreadStop = false;
            _writeThread = new System.Threading.Thread(WriteData);
            _writeThread.Start();
            System.Threading.Thread.Sleep(100);
        }


        public void Close()
        {
            _writeThreadStop = true;
            System.Threading.Thread.Sleep(100);
            _device.Close();
            System.Threading.Thread.Sleep(100);

        }

        public string GetConnectedDeviceInfo()
        {
            if (!_device.IsOpen)
            {
                return "not open";
            }
            else
            {
                _device.GetDescription(out string description);

                FTDI.FT_DEVICE device = FTDI.FT_DEVICE.FT_DEVICE_UNKNOWN;
                _device.GetDeviceType(ref device);

                _device.GetSerialNumber(out string serialNumber);
                return $"{description} ({device}) - Port: {serialNumber}";
            }
        }

        public void WriteData()
        {
            try
            {
                InitOpenDMX();

                while (!_writeThreadStop)
                {
                    var status = _device.SetBreak(true);
                    if (status != FTDI.FT_STATUS.FT_OK)
                        throw new Exception("error setting break on- status: " + status.ToString());

                    status = _device.SetBreak(false);
                    if (status != FTDI.FT_STATUS.FT_OK)
                        throw new Exception("error setting break off - status: " + status.ToString());

                    uint bytesWritten = 0;

                    status = _device.Write(_dmxBuffer, _dmxBuffer.Length, ref bytesWritten);

                    System.Threading.Thread.Sleep(DMX_SLEEP);

                }               


            }
            catch(Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void InitOpenDMX()
        {
            FTDI.FT_STATUS status;

            status = _device.ResetDevice();
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error resetting the device - status: " + status.ToString());

            status = _device.SetBaudRate(DEV_BAUDRATE);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error setting baudrate - status: " + status.ToString());

            status = _device.SetDataCharacteristics(DEV_DATA_BITS, DEV_STOP_BITS, DEV_PARITY);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error setting data characteristics - status: " + status.ToString());

            status = _device.SetFlowControl(DEV_FLOW, 0, 0);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error setting flow control - status: " + status.ToString());

            status = _device.SetRTS(false);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error clearing RTS - status: " + status.ToString());

            status = _device.Purge(DEV_PURGE_TX);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error purging tx - status: " + status.ToString());

            status = _device.Purge(DEV_PURGE_RX);
            if (status != FTDI.FT_STATUS.FT_OK)
                throw new Exception("error purging rx - status: " + status.ToString());

        }

    }
}
