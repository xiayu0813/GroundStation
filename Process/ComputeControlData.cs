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

        public int Compute(int[] MeasureData)
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


    public class ComputeControlData : INotifyPropertyChanged
    {
        public ComputeDirection ComputeDirectionX = new ComputeDirection();
        public ComputeDirection ComputeDirectionZ = new ComputeDirection();
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _XDirection;
        public int XDirection
        {
            get
            {
                return _XDirection;
            }
            set
            {
                _XDirection = value;
                NotifyPropertyChanged("XDirection");
            }
        }
        private int _ZDirection;
        public int ZDirection
        {
            get
            {
                return _ZDirection;
            }
            set
            {
                _ZDirection = value;
                NotifyPropertyChanged("ZDirection");
            }
        }

        private Queue<AirCraftState> _qRecentState = new Queue<AirCraftState>();
        public Queue<AirCraftState> qRecentState
        {
            get
            {
                return _qRecentState;
            }
            set
            {
                while( _qRecentState.Count >= 10)
                {
                    _qRecentState.Dequeue();
                }
                _qRecentState = value;
            }
        }

        public void Compute()
        {
            XDirection = ComputeDirectionX.Compute();
        }
    }
}
