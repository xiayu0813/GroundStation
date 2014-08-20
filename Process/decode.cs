using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GroundStation
{
    public class decode
    {
        bool NewFrameFlag = false; //用于表示接收到帧头
        uint FrameLength = 0; //帧长
        uint FrameCount = 0; //计数器
        byte FrameCellData = 0; //接收到的内容
        byte[] FrameContent = new byte[1024];
        byte[] FrameRealContent = new byte[1024];
        Thread thread;
        /// <summary>
        /// 开始解码
        /// </summary>
        public void Start()
        {
            thread = new Thread(Run);
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();
        }

        /// <summary>
        /// 转换数据，将原始数据中的0xEE0xEE转为0xFF,0xEE0xDD转为0xEE
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        private void DataConverter(byte[] source, byte[] target)
        {
            int j=0;
            for(int i = 0; i < source.Length; i++)
            {
                if (source[i] == 0xEE)
                {
                    switch (source[i + 1])
                    {
                        case 0xEE:
                            target[j++] = 0xFF;
                            break;
                        case 0xDD:
                            target[j++] = 0xEE;
                            break;
                        default:
                            break;
                    }
                    i++;
                }
                else
                    target[j++] = source[i];
            }
        }
        
        /// <summary>
        /// 检查校验码是否正确
        /// </summary>
        /// <param name="TestObject"></param>
        /// <returns></returns>
        private bool TestChecksum(byte[] TestObject)
        {
            byte CheckResualt = 0;
            foreach(byte item in TestObject)
            {
                CheckResualt ^= item; 
            }

            if (CheckResualt == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 将4byte的数据转换为int
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private int byte2int(byte[] source, uint StartIndex)
        {
            int resualt = 0;

            for (uint i = StartIndex+3; i >= StartIndex; i-- )
            {
                resualt <<= 8;
                resualt += source[i];
            }
            return resualt;
        }

        private void Run()
        {
            while (true)
            {
                
                lock (GroundStationCore.LockObject)
                {
                    if (GroundStationCore.qRawData.Count > 0)
                        FrameCellData = GroundStationCore.qRawData.Dequeue(); //取出接收队最开始的内容
                    else
                        continue; //if there is no data in qRawData,continue to the next loop
                }

                if(FrameCellData == 0xff)
                {//数据是启始码
                    NewFrameFlag = true; //表示一帧的开始
                    FrameLength = 0; //帧长清零
                    FrameCount = 0; //计数器清零
                    continue; //继续取下一个数据
                }

                if(NewFrameFlag)
                {//如果上一帧是帧头
                    NewFrameFlag = false; //清空标志
                    FrameLength = FrameCellData; //这一个数据表示帧长
                    FrameCount = 0; //计数器清零
                    continue; //继续取下一个数据
                }

                if( FrameLength > 0)
                {
                    FrameContent[FrameCount++] = FrameCellData; //先将数据存下来

                    if( FrameCount == FrameLength)
                    {//这一帧数据存储完毕
                        DataConverter(FrameContent,FrameRealContent); //转换数据

                        if(TestChecksum(FrameRealContent))
                        {//校验成功
                            GroundStationCore.AirCraftState.XAxis = byte2int(FrameRealContent,1);
                            GroundStationCore.AirCraftState.YAxis = byte2int(FrameRealContent,5);
                            GroundStationCore.AirCraftState.ZAxis = byte2int(FrameRealContent,9);
                        }

                        FrameLength = 0;
                        FrameCount = 0;
                    }// if(FrameCount == FrameLength)
                }//if( FrameLength > 0)
            }//while (GroundStationCore.qRawData.Count > 0)
        }

    }


}
