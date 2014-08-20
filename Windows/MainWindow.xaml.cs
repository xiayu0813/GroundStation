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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.IO;

namespace GroundStation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //binding the data to textbox

            //received orginal data
            Binding bindingRecvData = new Binding("Value");
            bindingRecvData.Source = GroundStationCore.SerialReceive;
            bindingRecvData.Path = new PropertyPath("RecvData");
            this.OrginalData.SetBinding(TextBox.TextProperty, bindingRecvData);

            //the x-axis data of aircraft state
            Binding bindingXAxis = new Binding("Value");
            bindingXAxis.Source = GroundStationCore.AirCraftState;
            bindingXAxis.Path = new PropertyPath("XAxis");
            this.StateX.SetBinding(TextBox.TextProperty, bindingXAxis);

            //the x-axis data of aircraft state
            Binding bindingYAxis = new Binding("Value");
            bindingYAxis.Source = GroundStationCore.AirCraftState;
            bindingYAxis.Path = new PropertyPath("YAxis");
            this.StateY.SetBinding(TextBox.TextProperty, bindingYAxis);
            //the x-axis data of aircraft state

            Binding bindingZAxis = new Binding("Value");
            bindingZAxis.Source = GroundStationCore.AirCraftState;
            bindingZAxis.Path = new PropertyPath("ZAxis");
            this.StateX.SetBinding(TextBox.TextProperty, bindingZAxis);
        }
       
        private void ShowConfigWindow(object sender, RoutedEventArgs e)
        {
            ConfigWindow win = new ConfigWindow();
            win.Owner = this;
            win.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GroundStationCore.SerialReceive.OpenRecvPort();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GroundStationCore.SerialReceive.RecvPort.Close();
        }
    }
}
