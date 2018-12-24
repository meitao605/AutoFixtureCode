using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AutoFixture
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Operator People_ID;
        private DispatcherTimer Get_ID = new DispatcherTimer();
        private DispatcherTimer Get_Order = new DispatcherTimer();
        private SerialPort Getid_Com = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
        private SerialPort Getid_Order = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);


        public MainWindow()
        {
            InitializeComponent();

            People_ID = new Operator();
            

            // Set Bind for Enter Id TextBox to People_ID.
            Binding PeopleID_Binding = new Binding
            {
                Source = People_ID,
                Path = new PropertyPath("OperID"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            this.Enter_ID.SetBinding(TextBox.TextProperty, PeopleID_Binding);
            Get_ID.Tick += Get_ID_Tick;
            Get_ID.Interval = TimeSpan.FromSeconds(0.1);
            Get_Order.Tick += Get_Order_Tick;
            Get_Order.Interval = TimeSpan.FromSeconds(0.1);

            Getid_Com.Open();

        }

        private void Get_Order_Tick(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Here's is trying to read the serial port to get the Scan Cards.
        /// This Timer will start only when the Mainpage = 0, and will stopped right after it changed to the next page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Get_ID_Tick(object sender, EventArgs e)
        {
           string str = "";
            str = Getid_Com.ReadExisting();
            if (str.Length>5)
            {
                People_ID.OperID = str;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage.SelectedIndex = 0;
            People_ID.Init();
            Get_ID.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.SelectedIndex = 0;
            People_ID.OperID = "";
            People_ID.Init();
            Get_ID.Start();
        }


        private void Enter_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (People_ID.OperID.Length > 8 && People_ID.StepNumber == 0)
            {
                MainPage.SelectedIndex = 1;
                People_ID.GotoStep1();
                Get_ID.Stop();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Getid_Com.Close();
        }
    }
}
