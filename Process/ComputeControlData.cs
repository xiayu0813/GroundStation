using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GroundStation
{
    public class ComputeDirection
    {
        public int Threshold { get; set; }

        int Compute()
        {
            int direction = 0;

            return direction;
        }
    }

    public class ProportionalControl
    {

    }

    public class DetermineControlMethod
    {

    }
    /// <summary>
    /// 根据飞行器的状态计算控制量，解码完成后调用
    /// </summary>
    public class ComputeControlData
    {
        public ComputeDirection ConputeDirectionX = new ComputeDirection();
        public ComputeDirection ConputeDirectionZ = new ComputeDirection();
        
    }
}
