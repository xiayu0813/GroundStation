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
            Config.Save();
            Close();

        }

        private void btConfigCancelCliked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if(ports.Length > 0)
            {
                cbRecvPortName.ItemsSource = ports;
                cbRecvPortName.SelectedIndex = 0;

                cbSendPortName.ItemsSource = ports;
                cbSendPortName.SelectedIndex = 1;
            }

        }

        private void cbRecvPortName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cbSendPortName.ItemsSource = ports;
                cbSendPortName.SelectedIndex = 1;
            }

        }

        private void cbSendPortName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                cbSendPortName.ItemsSource = ports;
                cbSendPortName.SelectedIndex = 1;
            }
        }
    }
}
