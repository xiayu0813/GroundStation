using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroundStation
{
    public class KalmanFilter
    {
        //卡尔曼滤波系数
        public double A{ get; set;}
        public double B{ get; set;}
        public double H{ get; set;}
        public double R{ get; set;}
        public double Q{ get; set;}

        //卡尔曼滤波中间量
        private double x_hat = 0;
        private double x_hat_predicted = 0;
        private double P_predicted = 0;
        private double K = 0;
        private double x_hat_last = 0;
        private double P_last = 0;
        private bool StartFlag = false;

        private void UpdateConfigdata()
        {
            A = GroundStationCore.Config.ZKalmanParaA;
            B = GroundStationCore.Config.ZKalmanParaB;
            H = GroundStationCore.Config.ZKalmanParaH;
            R = GroundStationCore.Config.ZKalmanParaR;
            Q = GroundStationCore.Config.ZKalmanParaQ;
        }
        public double FilterProcess(double FilterData,double u_last)
        {
            UpdateConfigdata();
            if (!StartFlag)
            {
                x_hat_last = FilterData;
                StartFlag = true;
            }
            else
            {
                x_hat_predicted = A * x_hat_last + B * u_last; //x的一步预测
                P_predicted = A * P_last * A * Q; //P的一步预测

                K = (P_predicted * H) / ( H * P_predicted * H * R);
                x_hat = x_hat_predicted + K * (FilterData - H * x_hat_predicted);

                P_last = (1 - H * K) * P_predicted;
                x_hat_last = x_hat;
            }
            return x_hat_last;
        }
    }
}
