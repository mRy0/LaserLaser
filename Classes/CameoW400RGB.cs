//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LaserControl
//{
//    public class CameoW400RGB
//    {
//        public enum Modes : byte
//        {
//            Off = 0,
//            Auto = 64,
//            Sound = 128,
//            DMX = 192
//        };

//        public enum Color : byte
//        {
//            RGB = 0,
//            Red = 64,
//            Green = 96,
//            Blue = 128,
//            Yellow = 160,
//            Pink = 192,
//            Cyan = 224,
//        };

//        public enum Pattern : byte
//        {
//            A = 0,
//            B = 8,
//            C = 16,
//            D = 24,
//            E = 32,
//            F = 40,
//            G = 48,
//            H = 56,
//            I = 64,
//            J = 72,
//            K = 80,
//            L = 88,
//            M = 96,
//            N = 104,
//            O = 112,
//            P = 120,
//            Q = 128,
//            R = 136,
//            S = 144,
//            T = 152,
//            U = 160,
//            V = 168,
//            W = 176,
//            X = 184,
//            Y = 192,
//            Z = 200,
//            AA = 208,
//            AB = 216,
//            AC = 224,
//            AD = 232,
//            AE = 240,
//            AF = 248
//        };

//        private byte _zooming;
//        private byte _xRolling;
//        private byte _yRolling;
//        private byte _zRolling;
//        private byte _xMoving;
//        private byte _yMoving;


//        private OpenDMX _openDmx;
//        private uint _channel;

//        public CameoW400RGB(uint channel, OpenDMX openDMX)
//        {
//            _channel = channel;
//            _openDmx = openDMX;
//        }

//        public void SetMode(Modes mode)
//        {
//            _openDmx.SetDMXValue(_channel, (byte)mode);
//        }

//        public void SetColor(Color color)
//        {
//            _openDmx.SetDMXValue(_channel + 1, (byte)color);
//        }

//        public void SetPattern(Pattern pattern)
//        {
//            _openDmx.SetDMXValue(_channel + 2, (byte)pattern);
//        }


//        public byte ZoomManual
//        {
//            set
//            {
//                _zooming = (byte) Math.Round((value / 2.0),0);
//                if (_zooming > 127) _zooming = 127;
//                _openDmx.SetDMXValue(_channel + 3, _zooming);
//            }
//            get
//            {
//                if (_zooming > 127) return 0;
//                else return (byte)(_zooming * 2);
//            }
//        }
//        public byte ZoomAuto
//        {
//            set
//            {                
//                _zooming =  (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 3, _zooming);
//            }
//            get
//            {
//                if (_zooming < 128) return 0;
//                else return (byte)((_zooming * 2)- 127);
//            }
//        }

//        public byte XRollingManual
//        {
//            set
//            {
//                _xRolling = (byte)Math.Round((value / 2.0), 0);
//                if (_xRolling > 127) _xRolling = 127;
//                _openDmx.SetDMXValue(_channel + 4, _xRolling);
//            }
//            get
//            {
//                if (_xRolling > 127) return 0;
//                else return (byte)(_xRolling * 2);
//            }
//        }
//        public byte XRollingSpeed
//        {
//            set
//            {
//                _xRolling = (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 4, _xRolling);
//            }
//            get
//            {
//                if (_xRolling < 128) return 0;
//                else return (byte)((_xRolling * 2) - 127);
//            }
//        }

//        public byte YRollingManual
//        {
//            set
//            {
//                _yRolling = (byte)Math.Round((value / 2.0), 0);
//                if (_yRolling > 127) _yRolling = 127;
//                _openDmx.SetDMXValue(_channel + 5, _yRolling);
//            }
//            get
//            {
//                if (_yRolling > 127) return 0;
//                else return (byte)(_yRolling * 2);
//            }
//        }
//        public byte YRollingSpeed
//        {
//            set
//            {
//                _yRolling = (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 5, _yRolling);
//            }
//            get
//            {
//                if (_yRolling < 128) return 0;
//                else return (byte)((_yRolling * 2) - 127);
//            }
//        }

//        public byte ZRollingManual
//        {
//            set
//            {
//                _zRolling = (byte)Math.Round((value / 2.0), 0);
//                if (_zRolling > 127) _zRolling = 127;
//                _openDmx.SetDMXValue(_channel + 6, _zRolling);
//            }
//            get
//            {
//                if (_zRolling > 127) return 0;
//                else return (byte)(_zRolling * 2);
//            }
//        }
//        public byte ZRollingSpeed
//        {
//            set
//            {
//                _zRolling = (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 6, _zRolling);
//            }
//            get
//            {
//                if (_zRolling < 128) return 0;
//                else return (byte)((_zRolling * 2) - 127);
//            }
//        }

//        public byte XMovingManual
//        {
//            set
//            {
//                _xMoving = (byte)Math.Round((value / 2.0), 0);
//                if (_xMoving > 127) _xMoving = 127;
//                _openDmx.SetDMXValue(_channel + 7, _xMoving);
//            }
//            get
//            {
//                if (_xMoving > 127) return 0;
//                else return (byte)(_xMoving * 2);
//            }
//        }
//        public byte XMovingSpeed
//        {
//            set
//            {
//                _xMoving = (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 7, _xMoving);
//            }
//            get
//            {
//                if (_xMoving < 128) return 0;
//                else return (byte)((_xMoving * 2) - 127);
//            }
//        }

//        public byte YMovingManual
//        {
//            set
//            {
//                _yMoving = (byte)Math.Round((value / 2.0), 0);
//                if (_yMoving > 127) _yMoving = 127;
//                _openDmx.SetDMXValue(_channel + 8, _yMoving);
//            }
//            get
//            {
//                if (_yMoving > 127) return 0;
//                else return (byte)(_yMoving * 2);
//            }
//        }
//        public byte YMovingSpeed
//        {
//            set
//            {
//                _yMoving = (byte)Math.Round(128 + (value / 2.0), 0);
//                _openDmx.SetDMXValue(_channel + 8, _yMoving);
//            }
//            get
//            {
//                if (_yMoving < 128) return 0;
//                else return (byte)((_yMoving * 2) - 127);
//            }
//        }




//    }

//}
