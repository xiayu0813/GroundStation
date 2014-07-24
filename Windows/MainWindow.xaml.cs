﻿using System;
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
        SerialReceive test = new SerialReceive();
        private const string Cfgfile = "GroundStationConfig.xml";
        public static Config Config;
        public MainWindow()
        {
            InitializeComponent();

            Binding binding = new Binding("Value");
            binding.Source = test;
            binding.Path = new PropertyPath("showContent");
            this.OrginalData.SetBinding(TextBox.TextProperty, binding);


            if (File.Exists(Cfgfile))
            {
                Config = Config.Load(Cfgfile);
            }
            else
            {
                Config = new Config();
                Config.Save(Config);
            }
        }
       
        private void ShowConfigWindow(object sender, RoutedEventArgs e)
        {
            ConfigWindow win = new ConfigWindow();
            win.Owner = this;
            win.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            test.OpenRecvPort();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            test.RecvPort.Close();
        }
    }
}