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
        public static Config Config;
        public static SerialReceive SerialReceive = new SerialReceive();


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
