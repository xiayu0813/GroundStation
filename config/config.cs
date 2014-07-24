using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.IO;
using System.Windows;

namespace GroundStation
{
    [DataContract]
    public class Config
    {
        [DataMember]
        public string SendPortName;
        [DataMember]
        public string RecvPortName;
 
        public Config()
        {
            SendPortName = "COM2";
            RecvPortName = "COM3";
        }

        public static Config Load(string file)
        {
            if (!System.IO.File.Exists(file))
                return new Config();
            try
            {
                var fs = new FileStream(file, FileMode.Open);
                var ser = new DataContractSerializer(typeof(Config));
                var cfg = (Config)ser.ReadObject(fs);
                fs.Close();
                return cfg;
            }
            catch
            {
                MessageBox.Show("加载配置文件遇到错误，使用默认配置");
                return new Config();
            }
        }
        public static void Save(Config cfg = null ,string file = "GroundStationConfig.xml")
        {
            if (cfg == null)
            {
                cfg = new Config();
            }
            var fs = new FileStream(file, FileMode.Create);
            var ser = new DataContractSerializer(typeof(Config));
            ser.WriteObject(fs, cfg);
            fs.Close();
        }
    }
}
