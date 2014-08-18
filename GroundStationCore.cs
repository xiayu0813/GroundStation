using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GroundStation
{
    public static class GroundStationCore
    {
        private const string Cfgfile = "GroundStationConfig.xml";
        public static Config Config; //配置数据
        public static SerialReceive SerialReceive = new SerialReceive(); //接收串口

        public static Queue<byte> qRawData = new Queue<byte>(); //接收到的原始数据
        public static object LockObject = new object();


        static GroundStationCore()
        {
            if (File.Exists(Cfgfile))
            {
                Config = Config.Load(Cfgfile);
            }
            else
            {
                Config = new Config();
                Config.Save();
            }
        }
    }
}
