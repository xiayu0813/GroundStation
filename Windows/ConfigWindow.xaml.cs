using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Ports;

namespace GroundStation
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {

        List<string> comList = new List<string>();
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private void btConfigOKCliked(object sender, RoutedEventArgs e)
        {
            GroundStationCore.Config.RecvPortName = cbRecvPortName.SelectedItem as string;
            GroundStationCore.Config.SendPortName = cbSendPortName.SelectedItem as string;
            GroundStationCore.Config.Save(); //保存配置
            GroundStationCore.SerialReceive.OpenRecvPort(); //重新打开串口
            Close();
        }

        private void btConfigCancelCliked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshPortName()
        {
            string[] ports = SerialPort.GetPortNames(); //获取可用串口
            if (ports.Length > 0)
            {
                cbRecvPortName.ItemsSource = ports;
                cbSendPortName.ItemsSource = ports;


                //获取所选串口的下标
                int index = Array.IndexOf(ports, GroundStationCore.Config.RecvPortName);
                
                //如果所选串口可用，则显示出，否则显示第一个串口
                if (index >= 0)
                    cbRecvPortName.SelectedIndex = index;
                else
                    cbRecvPortName.SelectedIndex = 0;

                index = Array.IndexOf(ports, GroundStationCore.Config.SendPortName);
                if (index >= 0)
                    cbSendPortName.SelectedIndex = index;
                else
                    cbSendPortName.SelectedIndex = 0;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GroundStationCore.SerialReceive.RecvPort.Close();
            GroundStationCore.Config.Load(); //读取配置文件
            RefreshPortName();
        }
    }
}
