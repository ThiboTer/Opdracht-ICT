using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using LCD;

namespace Opdracht_ICT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _serialPort;
        tekstLCD LCD1;
        public MainWindow()
        {
            InitializeComponent();
            LCD1 = new tekstLCD();
            _serialPort = new SerialPort();

            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);
        }

       

        private void cbxComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (cbxComPorts.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxComPorts.SelectedItem.ToString();
                    _serialPort.Open();
                }
            }

        }

       private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBox.Text;
            LCD1.Tekst=text;
            if (LCD1.lengte == false)
            {
                _serialPort.WriteLine(LCD1.Tekst.PadRight(16, ' '));
                foutmelding.Visibility = Visibility.Collapsed;
            }
            else
            {
                foutmelding.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _serialPort.Close();
        }
    }
}
