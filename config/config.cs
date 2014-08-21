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
        //接收发送串口号
        [DataMember]
        public string SendPortName;
        [DataMember]
        public string RecvPortName;

        //Z轴Kalman滤波参数
        [DataMember]
        public double ZKalmanParaA;
        [DataMember]
        public double ZKalmanParaB;
        [DataMember]
        public double ZKalmanParaH;
        [DataMember]
        public double ZKalmanParaR;
        [DataMember]
        public double ZKalmanParaQ;

 
 
        public Config()
        {
            SendPortName = "COM2";
            RecvPortName = "COM3";

            ZKalmanParaA = 1;
            ZKalmanParaB = 0;
            ZKalmanParaH = 1;
            ZKalmanParaQ = 0.2;
            ZKalmanParaR = 12;

        }

        public static Config Load(string file = "GroundStationConfig.xml")
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
                cfg = GroundStationCore.Config;
            }
            var fs = new FileStream(file, FileMode.Create);
            var ser = new DataContractSerializer(typeof(Config));
            ser.WriteObject(fs, cfg);
            fs.Close();
        }
    }
}
