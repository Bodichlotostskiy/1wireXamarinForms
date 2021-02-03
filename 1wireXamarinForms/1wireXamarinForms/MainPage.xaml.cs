using _1wireXamarinForms.DalSemi.OneWire;
using _1wireXamarinForms.DalSemi.OneWire.Adapter;
using _1wireXamarinForms.DalSemi.Serial;
using _1wireXamarinForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SerialPort = DalSemi.Serial.SerialPort;
using AccessProvider = DalSemi.OneWire.AccessProvider;
using PortAdapter = DalSemi.OneWire.Adapter.PortAdapter;


namespace _1wireXamarinForms
{
    public partial class MainPage : ContentPage
    {
        public List<MyDevice> Devices { get; set; }

        public SerialPort port;
        public PortAdapter adapter;
        public List<string> DeviceNameEvalible = new List<string>(10);

        public MainPage(List<MyDevice> devices)
        {

            InitializeComponent();
            Devices = devices;
            (Application.Current).MainPage = new NavigationPage(new MainPage(Checkport()));
            this.BindingContext = this;
        }
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

        public void OnRefreshDevicesButtonClicked(object sender, EventArgs args)
        {
            (Application.Current).MainPage = new NavigationPage(new MainPage(Checkport()));
        }

        public List<MyDevice> Checkport()
        {
            List<MyDevice> newDeviceList = new List<MyDevice>();

            //List<PortAdapter> portAdapterList =(List<PortAdapter>)AccessProvider.GetAllAdapters();
            //if (!portAdapterList.Any())
               // newDeviceList.Add(new MyDevice() { DeviceName = "No Devices Found", Description = "try late" });

            //adapter = portAdapterList.FirstOrDefault();


            //if (adapter == null)
                newDeviceList.Add(new MyDevice() { DeviceName = "No Devices Found", Description = "try late" });

            //DeviceNameEvalible = portAdapterList.Select(x => x.AdapterName).ToList();
            //try
            //{
            //    adapter = AccessProvider.GetAdapter("{DS9490}");
            //    if (adapter.OpenPort(port.PortName))
            //        newDeviceList.Add(new MyDevice() { DeviceName = "Serial Port is Open", Description = "YRA!!!DS9490}" });
            //}
            //catch
            //{
            //    adapter = AccessProvider.GetAdapter("{DS9097U}");
            //    if (adapter.OpenPort(port.PortName))
            //        newDeviceList.Add(new MyDevice() { DeviceName = "Serial Port is Open", Description = "YRA!!!{DS9097U}" });
            //}

            port.Open();

            if(port.IsOpen)
                newDeviceList.Add(new MyDevice() { DeviceName = "Serial Port is Open", Description = "YRA!!!" });

            if (DeviceNameEvalible.Any())
                for (int i = 0; i < DeviceNameEvalible.Count(); i++)
                    newDeviceList.Add(new MyDevice() { DeviceName = DeviceNameEvalible[i], Description = $"empty string {i+1} device" });



            return newDeviceList;
        }
    }
}
