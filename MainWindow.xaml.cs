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
using Microsoft.Win32;
using System.ComponentModel;


namespace ComPort
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static SerialPort port;
        public static int LEDCount = 120;

        public MainWindow()
        {
            InitializeComponent();
            Closing += OnWindowCloasing;

            String[] portNames = SerialPort.GetPortNames();
            foreach (String port in portNames)
            {
                comList.Items.Add(port);
            }

        }
        public void OnWindowCloasing(object sender, CancelEventArgs e)
        {
            if(port != null) port.Close();
        }

        private void ConnectingBT_Click(object sender, RoutedEventArgs e)
        {
            if (port == null)
            {
                if (comList.SelectedItem == null) MessageBox.Show("Choose the goddamn COMPORT!");
                else
                {
                    String comPortName = comList.SelectedItem.ToString();
                    port = new SerialPort(comPortName, 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                    port.ReadTimeout = 500;
                    port.WriteTimeout = 500;
                    port.Open();
                    ConnectingBT.Content = "Disconnect";
                }
            }
            else
            {
                port.Close();
                port = null;
                ConnectingBT.Content = "Connect";
            }
        }

        public void sendDataToSerialPort(byte[] data,int len)//array length should be 3 times bigger than LEDCount
        {
            port.Write(data, 0, len);
        }
    }
}
