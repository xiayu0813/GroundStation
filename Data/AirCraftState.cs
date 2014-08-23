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

        private int _XAxis;
        public int XAxis 
        {
            get
            {
                return _XAxis;
            }
            set
            {
                _XAxis = value;
                NotifyPropertyChanged("XAxis");
            }
        }

        private int _YAxis;

        public int YAxis
        {
            get
            {
                return _YAxis;
            }
            set
            {
                _YAxis = value;
                NotifyPropertyChanged("YAxis");
            }
        }


        private int _ZAxis;
        public int ZAxis
        {
            get
            {
                return _ZAxis;
            }
            set
            {
                _ZAxis = value;
                NotifyPropertyChanged("ZAxis");
            }
        }
    }
}
