using _1wireXamarinForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO.Ports;
namespace _1wireXamarinForms
{
    public partial class MainPage : ContentPage
    {
        public List<MyDevice> Devices { get; set; }

        public SerialPort port;
        public MainPage()
        {
            InitializeComponent();
            Devices = new List<MyDevice>
            {
                new MyDevice {DeviceName="ds1922l",Description="this is device ds1922l" },
                new MyDevice {DeviceName="ds1923",Description="this is device ds1923"},
                new MyDevice {DeviceName="ds1992",Description="this is device ds1992" }
            };
            this.BindingContext = this;

        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MyDevice selectedDevice = e.Item as MyDevice;
            if (selectedDevice != null)
                await DisplayAlert("Выбранная модель", $"{selectedDevice.DeviceName} - {selectedDevice.Description}", "OK");
        }

        public async void OnRefreshDevicesButtonClicked(object sender, EventArgs args)
        {
            checkport();

        }

        public async void  checkport()
        {
            port = new SerialPort(port.PortName=SetPortName(port.PortName), 9600, Parity.None, 0, StopBits.One);

            port.Open();



        }

        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }


    }
}
