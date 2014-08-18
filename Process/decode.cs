using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GroundStation.Process
{
    public class decode
    {
        bool NewFrameFlag = false; //用于表示接收到帧头
        uint FrameLength = 0; //帧长
        uint FrameCount = 0; //计数器
        byte FrameContent = 0; //接收到的内容


        /// <summary>
        /// 开始解码
        /// </summary>
        private void Start()
        {
            var thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            while (GroundStationCore.qRawData.Count > 0)
            {
                lock (GroundStationCore.LockObject)
                {
                    FrameContent = GroundStationCore.qRawData.Dequeue(); //取出接收队最开始的内容
                }

                if(FrameContent == 0xff)
                {//数据是启始码
                    NewFrameFlag = true; //表示一帧的开始
                    FrameLength = 0; //帧长清零
                    FrameCount = 0; //计数器清零
                    continue; //继续取下一个数据
                }

                if(NewFrameFlag)
                {//如果上一帧是帧头
                    NewFrameFlag = false; //清空标志
                    FrameLength = FrameContent; //这一个数据表示帧长
                    FrameCount = 0; //计数器清零
                    continue; //继续取下一个数据
                }
            }
        }
    }
}
