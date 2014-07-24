using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;

namespace GroundStation
{
    public class SerialReceive : INotifyPropertyChanged
    {//串口接收数据

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string ShowContent;
        public string showContent
        {
            get
            {
                return ShowContent;
            }
            set
            {
                    this.ShowContent = value;
                    NotifyPropertyChanged("showContent");
            }
        }

        public SerialPort RecvPort = new SerialPort(); //定义接收串口，不配置
        public string RecvPortName = "COM2"; //串口号
        bool RecvPortIsOpen = false; //串口开启状态

        public Queue<byte> RecvQueue = new Queue<byte>();
        public object LockObject = new object();

        /// <summary>
        /// 打开接收串口
        /// </summary>
        /// <returns>
        /// 返回串口是否成功找开
        /// </returns>
        public bool OpenRecvPort()
        {
            RecvPort.PortName = GroundStationCore.Config.RecvPortName; //串口号
            RecvPort.BaudRate = 9600; // 波特率
            RecvPort.Parity = Parity.None; //校验位
            RecvPort.DataBits = 8; //数据位
            RecvPort.StopBits = StopBits.One; //停止位

            if (RecvPortIsOpen == true)//判断串口是否已经打开
            {//串口已经打开
                RecvPort.Close(); //关闭串口
            }

            RecvPort.Open(); //打开串口
            if (RecvPort.IsOpen == true)
            {
                RecvPortIsOpen = true;
                RecvPort.DataReceived += RecvPort_DataReceived;
                return true; // 已经打开，返回true 
            }
            else
                return false; // 没有打开，返回false
        }

        void RecvPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {//test
            byte[] RecvBuffer; //接收缓冲区
            RecvBuffer = new byte[RecvPort.BytesToRead]; //接收缓存的大小
            RecvPort.Read(RecvBuffer, 0, RecvBuffer.Length); //读取数据
            lock (LockObject) // 在保存数据的时候锁定
            {
                for (int i = 0; i < RecvBuffer.Length; i++) //读取到的数据入列
                {
                    RecvQueue.Enqueue(RecvBuffer[i]);
                    showContent += RecvBuffer[i].ToString();
                    showContent += " ";
                }
            }
        }

    }
}
