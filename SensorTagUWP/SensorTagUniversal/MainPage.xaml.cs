using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using Windows.UI.Popups;
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Exceptions;
using X2CodingLab.SensorTag.Sensors;
using System.Threading.Tasks;
using SensorTagUniversal.Entities;
using Newtonsoft.Json;
using System.Text;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Microsoft.Azure.Devices.Client;
using Windows.Networking.Sockets;
//using SensorTagUniversal.

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SensorTagUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StreamSocketListener tcpListener;
        private StreamSocket connectedSocket = null;
        private const string port = "9090";

        IRTemperatureSensor tempSen;
        HumiditySensor humidSen;
        static DeviceClient deviceClient;
        static string iotHubUri;
        static string deviceKey;
        static string city = "Chicago";
        bool isIoTHubEnabled = false;

        public MainPage()
        {
            this.InitializeComponent();
            StartApp();
            InitializeSockerListener();
        }
        private void Refresh_click(object sender, RoutedEventArgs e)
        {
            StartApp();
        }
        private async void StartApp()
        {
            Exception exc = null;

            try
            {
                
                using (DeviceInfoService dis = new DeviceInfoService())
                {
                    await dis.Initialize();
                    //await new MessageDialog("Device Connected").ShowAsync();
                    serialNumber.Text = "ID: " + await dis.ReadSystemId();
                    modelNumber.Text = await dis.ReadModelNumber();
                    // serialNumber.Text = await dis.ReadSerialNumber();
                    //tbFWRev.Text = "Firmware Revision: " + await dis.ReadFirmwareRevision();
                    //tbHWRev.Text = "Hardware Revision: " + await dis.ReadHardwareRevision();
                    //tbSWRev.Text = "Sofware Revision: " + await dis.ReadSoftwareRevision();
                    //tbManufacturerName.Text = "Manufacturer Name: " + await dis.ReadManufacturerName();
                    //tbCert.Text = "Cert: " + await dis.ReadCert();
                    //tbPNP.Text = "PNP ID: " + await dis.ReadPnpId();
                    InitializeDevice();
                }
            }
            catch (Exception ex)
            {
                exc = ex;
                await new MessageDialog(exc.Message).ShowAsync();
            }

            if (exc != null)
                await new MessageDialog(exc.Message).ShowAsync();
        }

        private void Initialize_click(object sender, RoutedEventArgs e)
        {
            InitializeDevice();
        }
        private async void InitializeDevice()
        {
            Exception exc = null;

            try
            {
                tempSen = new IRTemperatureSensor();
                await tempSen.Initialize();
                await tempSen.EnableSensor();
                await tempSen.EnableNotifications();
                tempSen.SensorValueChanged += SensorValueChanged;

                humidSen = new HumiditySensor();
                await humidSen.Initialize();
                await humidSen.EnableSensor();
                await humidSen.EnableNotifications();
                humidSen.SensorValueChanged += SensorValueChanged;
                
                //await new MessageDialog("Device Connected").ShowAsync();
                //ReadDeviceData();
                ReadData.IsEnabled = true;
                ConnectIoTHub.IsEnabled = true;
            }
            catch (Exception ex)
            {
                exc = ex;
                await new MessageDialog(exc.Message).ShowAsync();
            }

            if (exc != null)
                await new MessageDialog(exc.Message).ShowAsync();

        }

        // Not used this method currently
        // Can be used to call the value changes in the Sensor
        async void SensorValueChanged(object sender, X2CodingLab.SensorTag.SensorValueChangedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                switch (e.Origin)
                {
                    case SensorName.Accelerometer:
                        double[] accValues = Accelerometer.CalculateCoordinates(e.RawData, 1 / 64.0);
                        //tbAccelerometer.Text = "X: " + accValues[0].ToString("0.00") + " Y: " + accValues[1].ToString("0.00") + " Z: " + accValues[2].ToString("0.00");
                        break;
                    case SensorName.Gyroscope:
                        float[] axisValues = Gyroscope.CalculateAxisValue(e.RawData, GyroscopeAxis.XYZ);
                        //tbGyroscope.Text = "X: " + axisValues[0].ToString("0.00") + " Y: " + axisValues[1].ToString("0.00") + " Z: " + axisValues[2].ToString("0.00");
                        break;
                    case SensorName.HumiditySensor:
                        humidityRes.Text = HumiditySensor.CalculateHumidityInPercent(e.RawData).ToString("0.00") + "%";
                        humidityRes.Visibility = Visibility.Visible;
                        humidityBlock.Visibility = Visibility.Visible;
                        break;
                    case SensorName.Magnetometer:
                        float[] magnetValues = Magnetometer.CalculateCoordinates(e.RawData);
                        // tbMagnetometer.Text = "X: " + magnetValues[0].ToString("0.00") + " Y: " + magnetValues[1].ToString("0.00") + " Z: " + magnetValues[2].ToString("0.00");
                        break;
                    case SensorName.PressureSensor:
                        try
                        {
                            //  tbPressure.Text = (PressureSensor.CalculatePressure(e.RawData, ps.CalibrationData) / 100).ToString("0.00");
                        }
                        catch (NullReferenceException)
                        {
                            // in case another(!) setup is executed, so ps is null
                        }
                        break;
                    case SensorName.SimpleKeyService:
                        if (SimpleKeyService.LeftKeyHit(e.RawData))
                        {
                            //   tbLeftKey.Text = "hit!";
                            await Task.Delay(200);
                            //   tbLeftKey.Text = "";
                        }
                        else if (SimpleKeyService.RightKeyHit(e.RawData))
                        {
                            // tbRightKey.Text = "hit!";
                            await Task.Delay(200);
                            //   tbRightKey.Text = "";
                        }
                        break;
                    case SensorName.TemperatureSensor:
                        double ambient = IRTemperatureSensor.CalculateAmbientTemperature(e.RawData, TemperatureScale.Farenheit);
                        double target = IRTemperatureSensor.CalculateTargetTemperature(e.RawData, ambient, TemperatureScale.Farenheit);
                        AmbientRes.Text = ambient.ToString("0.00");
                        TargetRes.Text = target.ToString("0.00");
                        AmbientRes.Visibility = Visibility.Visible;
                        TargetRes.Visibility = Visibility.Visible;
                        temp1_Copy1.Visibility = Visibility.Visible;
                        temp2_Copy.Visibility = Visibility.Visible;

                        break;
                }
            });
        }

        private void ReadTemperature_click(object sender, RoutedEventArgs e)
        {
            ReadDeviceData();
        }
        private async void ReadDeviceData()
        {
            Sensor S = new Sensor();
           // deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("SensorTag", deviceKey));
            Exception exc = null;

            try
            {
                byte[] tempValue = await tempSen.ReadValue();
                double ambientTemp = IRTemperatureSensor.CalculateAmbientTemperature(tempValue, TemperatureScale.Farenheit);
                double targetTemp = IRTemperatureSensor.CalculateTargetTemperature(tempValue, ambientTemp, TemperatureScale.Farenheit);

                byte[] tempValue1 = await humidSen.ReadValue();
                string humidity = HumiditySensor.CalculateHumidityInPercent(tempValue1).ToString("0.00") + "%";
                humidityRes.Text = humidity;
                humidityBlock.Visibility = Visibility.Visible;
                humidityRes.Visibility = Visibility.Visible;

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    AmbientRes.Text = ambientTemp.ToString("0.00");
                    TargetRes.Text = targetTemp.ToString("0.00");
                    AmbientRes.Visibility = Visibility.Visible;
                    TargetRes.Visibility = Visibility.Visible;
                    temp1_Copy1.Visibility = Visibility.Visible;
                    temp2_Copy.Visibility = Visibility.Visible;
                });
            }
            catch (Exception ex)
            {
                exc = ex;
                await new MessageDialog(exc.Message).ShowAsync();
            }
        }

        private async void SendIoT_click(object sender, RoutedEventArgs e)
        {
            if (IoTHubUri.Text == "" && IoTDeviceKey.Text == "")
            {
                await new MessageDialog("Please Enter Iothub URI and Device key").ShowAsync();
            }
            else
            {
                iotHubUri = IoTHubUri.Text;
                deviceKey = IoTDeviceKey.Text;
                StopIoTHub.IsEnabled = true;
                IoTHubSentMsgText.Visibility = Visibility;
                Sensor S = new Sensor();              
                Exception exc = null;
                isIoTHubEnabled = true;

                try
                {
                    deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("SensorTag", deviceKey));
                    while (true)
                    {
                        if (isIoTHubEnabled == false)
                        {
                            break;
                        }
                        byte[] tempValue = await tempSen.ReadValue();
                        double ambientTemp = IRTemperatureSensor.CalculateAmbientTemperature(tempValue, TemperatureScale.Farenheit);
                        double targetTemp = IRTemperatureSensor.CalculateTargetTemperature(tempValue, ambientTemp, TemperatureScale.Farenheit);

                        byte[] tempValue1 = await humidSen.ReadValue();
                        double humidity = HumiditySensor.CalculateHumidityInPercent(tempValue1);
                        /// double targetTemp = IRTemperatureSensor.CalculateTargetTemperature(tempValue, ambientTemp, TemperatureScale.Celsius);
                        humidityRes.Text = humidity.ToString("0.00") + "%";
                        humidityBlock.Visibility = Visibility.Visible;
                        
                                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            AmbientRes.Text = ambientTemp.ToString("0.00");
                            TargetRes.Text = targetTemp.ToString("0.00");
                            temp1_Copy1.Visibility = Visibility.Visible;
                            temp2_Copy.Visibility = Visibility.Visible;
                        });
                        var telemetryDataPoint = S.Generate(modelNumber.Text, ambientTemp, humidity, city);
                        var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                        var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
                        await deviceClient.SendEventAsync(message);
                        IoTHubSentMsgText.Text = messageString;

                        //  LinearGraph.x
                        // List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
                        //  financialStuffList.Add(new FinancialStuff() { Name = "Sensor", Amount = ambientTemp});
                        // (LineChart.Series[0] as LineSeries).ItemsSource = financialStuffList;
                        // await deviceClient.SendEventAsync(message);
                        //Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                        await Task.Delay(TimeSpan.FromSeconds(2));
                    }
                }
                catch (Exception ex)
                {
                    exc = ex;
                    await new MessageDialog(exc.Message).ShowAsync();
                }
            }

        }

        private void ConnectIoT_click(object sender, RoutedEventArgs e)
        {
            IoTHubUri.Visibility = Visibility;
            IoTDeviceKey.Visibility = Visibility;
            SendIoTHub.IsEnabled = true;
            IoTHubUri.IsEnabled = true;
            IoTDeviceKey.IsEnabled = true;
        }

        private void StopIoT_click(object sender, RoutedEventArgs e)
        {
            isIoTHubEnabled = false;
            IoTHubSentMsgText.Text = "Stopped";
        }

        private void IoTHubUri_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if(Io)
        }

        private void IoTHubUri_LostFocus(object sender, TextChangedEventArgs e)
        {

        }

        private async void InitializeSockerListener()
        {
            tcpListener = new StreamSocketListener();
            tcpListener.ConnectionReceived += OnConnected;
            await tcpListener.BindEndpointAsync(null, port);
        }

        private async void OnConnected(
           StreamSocketListener sender,
           StreamSocketListenerConnectionReceivedEventArgs args)
        {
            if (connectedSocket != null)
            {
                connectedSocket.Dispose();
                connectedSocket = null;
            }
            connectedSocket = args.Socket;
            //await SendMessageTextBox.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            //{
            //    rootPage.NotifyUser("Client has connected, go ahead and send a message...", NotifyType.StatusMessage);
            //    SendMessageTextBox.IsEnabled = true;
            //    SendButton.IsEnabled = true;
            //});
        }
    }
}
