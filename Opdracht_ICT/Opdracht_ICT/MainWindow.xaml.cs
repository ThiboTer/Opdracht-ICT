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
using System.Windows.Threading;
using LCD;

namespace Opdracht_ICT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // instelen breedte LCD display
        public int LCDbreedte = 16;

        private bool isButtonEnabled = true;
        private bool clockButton = false;
        private string laatsetijd;

        SerialPort _serialPort;
        tekstLCD lijn1;
        tekstLCD lijn2;
        DispatcherTimer buttonTimer;
        DispatcherTimer clockTimer;
        public MainWindow()
        {
            InitializeComponent();

            lijn1 = new tekstLCD();
            lijn2 = new tekstLCD();

            _serialPort = new SerialPort();
            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);

            TextBox.IsEnabled = false;
            TextBox2.IsEnabled = false;
            Button.IsEnabled = false;
            Remove.IsEnabled = false;
            clock.IsEnabled = false;

            buttonTimer = new DispatcherTimer();
            buttonTimer.Tick += ButtonTimer_Tick;
            buttonTimer.Interval = TimeSpan.FromSeconds(3);

            clockTimer = new DispatcherTimer();
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Interval = TimeSpan.FromSeconds(1);
            clockTimer.Start();


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
                    TextBox.IsEnabled = true;
                    TextBox2.IsEnabled = true;
                    Button.IsEnabled = true;
                    Remove.IsEnabled = true;
                    clock.IsEnabled = true;
                    tekstNaarLCD(lijn1.Tekst, lijn2.Tekst);
                }
                else 
                {
                    TextBox.IsEnabled = false;
                    TextBox2.IsEnabled = false;
                    Button.IsEnabled = false;
                    Remove.IsEnabled = false;
                    clock.IsEnabled = false;
                }
            }

        }

       private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonEnabled) 
            {
                clockButton = false;
                foutmelding.Visibility = Visibility.Collapsed;
                string text = TextBox.Text;
                string text2 = TextBox2.Text;
                lijn1.Tekst=text;
                lijn2.Tekst=text2;
                tekstNaarLCD(lijn1.Tekst, lijn2.Tekst);
                isButtonEnabled = false;
                buttonTimer.Start();
            }
           else
                foutmelding.Visibility = Visibility.Visible; 
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonEnabled)
            {
                clockButton = false;
                foutmelding.Visibility = Visibility.Collapsed;
                lijn1.VerwijderTekst();
                lijn2.VerwijderTekst();
                TextBox.Text = "";
                TextBox2.Text = "";
                tekstNaarLCD(lijn1.Tekst, lijn2.Tekst);
                isButtonEnabled = false;
                buttonTimer.Start();
            }
            else
                foutmelding.Visibility = Visibility.Visible; 
        }

        private void clock_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonEnabled)
            {
                clockButton = true;
                lijn1.UpdateDateTime();
                lijn2.UpdateDateTime();
                laatsetijd = lijn1.Uur;
                tekstNaarLCD(lijn1.Uur, lijn2.Datum);
                isButtonEnabled = false;
                buttonTimer.Start();
            }
            else
                foutmelding.Visibility = Visibility.Visible; 
        
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            
            if (clockButton)
            {
                lijn1.UpdateDateTime();
                lijn2.UpdateDateTime();
                if (lijn1.Uur!= laatsetijd) 
                {
                    laatsetijd = lijn1.Uur;
                    tekstNaarLCD(lijn1.Uur, lijn2.Datum); 
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _serialPort.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox.MaxLength = LCDbreedte;
            size(TextBox,size1);
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox2.MaxLength = LCDbreedte;
            size(TextBox2, size2);
        }

        private void size( TextBox box ,Label test)
        {
            string tekst =box.Text;
            int teller = tekst.Length;
            test.Content = teller+"/"+ LCDbreedte;
        }

        private void ButtonTimer_Tick(object sender, EventArgs e)
        {
            isButtonEnabled = true;
            buttonTimer.Stop();
        }

        private void tekstNaarLCD(string lijn1, string lijn2)
        {
            _serialPort.WriteLine(lijn1.PadRight(LCDbreedte, ' '));
            _serialPort.WriteLine(lijn2.PadRight(LCDbreedte, ' '));
        }

       
    }
}
