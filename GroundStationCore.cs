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
        public static SerialReceive SerialReceive = new SerialReceive();

        public static Queue<byte> qRawData = new Queue<byte>(); //接收到的原始数据
        public static object LockObject = new object();

        public static AirCraftState AirCraftCurrentState = new AirCraftState();
        public static AirCraftState TargetPositon = new AirCraftState()
        {
            XAxis = Config.TargetPositionX,
            YAxis = Config.TargetPositionY,
            ZAxis = Config.TargetPositionZ
        };
        public static decode decode = new decode();
        
        public static KalmanFilter ZAxisKF = new KalmanFilter();

        public static ControlData ControlData = new ControlData();
        public static ComputeControlData ComputeControlData = new ComputeControlData();


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
            UpdateConfigData();
        }

        static public void UpdateConfigData()
        {
            TargetPositon.XAxis = Config.TargetPositionX;
            TargetPositon.YAxis = Config.TargetPositionY;
            TargetPositon.ZAxis = Config.TargetPositionZ;

            ZAxisKF.A = Config.ZKalmanParaA;
            ZAxisKF.B = Config.ZKalmanParaB;
            ZAxisKF.H = Config.ZKalmanParaH;
            ZAxisKF.R = Config.ZKalmanParaR;
            ZAxisKF.Q = Config.ZKalmanParaQ;
        }
    }
}
