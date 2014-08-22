using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GroundStation
{
    public class ControlData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _ChannelA;
        public int ChannelA
        {
            get
            {
                return _ChannelA;
            }
            set
            {
                _ChannelA = value;
                NotifyPropertyChanged("ChannelA");
            }
        }

        private int _ChannelE;
        public int ChannelE
        {
            get
            {
                return _ChannelE;
            }

            set
            {
                _ChannelE = value;
                NotifyPropertyChanged("ChannelE");
            }    
        }

        
        private int _ChannelT;
        public int ChannelT
        {
            get
            {
                return _ChannelT;
            }
            set
            {
                _ChannelT = value;
                NotifyPropertyChanged("ChannelT");
            }    
        }

        private int _ChannelR;
        public int ChannelR
        {
            get
            {
                return _ChannelR;
            }
            set
            {
                _ChannelR = value;
                NotifyPropertyChanged("ChannelR");
            }    
        }
    }
}
