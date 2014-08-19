using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GroundStation
{
    /// <summary>
    /// 飞行器的姿态XYZ三个方向
    /// </summary>
    public class AirCraftState : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int xAxis;
        public int XAxis 
        {
            get
            {
                return xAxis;
            }
            set
            {
                xAxis = value;
                NotifyPropertyChanged("XAis");
            }
        }

        private int yAxis;

        public int YAxis
        {
            get
            {
                return yAxis;
            }
            set
            {
                yAxis = value;
                NotifyPropertyChanged("YAxis");
            }
        }


        private int zAxis;
        public int ZAxis
        {
            get
            {
                return zAxis;
            }
            set
            {
                zAxis = value;
                NotifyPropertyChanged("ZAxis");
            }
        }
    }
}
