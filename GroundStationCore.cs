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

        public static AirCraftState AirCraftState = new AirCraftState();//飞行器的当前位置
        public static AirCraftState TargetPositon = new AirCraftState();//控制的目标位置
        public static decode decode = new decode();


        //Z轴的卡尔曼滤波
        public static KalmanFilter ZAxisKF = new KalmanFilter();

        //控制数据
        public static ControlData ControlData = new ControlData();

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
