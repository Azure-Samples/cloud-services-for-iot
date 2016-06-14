using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;
using SimulatedDevice.Entities;
using System.Configuration;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Exceptions;

namespace SimulatedDevice
{
    class Program
    {       
        static string iotHubUri;
        static string deviceName;
        static string deviceKey;
        static DeviceClient deviceClient;
        static void Main(string[] args)
        {
            iotHubUri = ConfigurationManager.AppSettings["IotHubUri"];
            deviceName = ConfigurationManager.AppSettings["deviceName"];
            // deviceKey = ConfigurationManager.AppSettings["DeviceKey"];

            IoTService iothub = new IoTService();
            // Add device to Iothub
            deviceKey = iothub.AddDeviceAsync(deviceName).Result;

            // Task<IEnumerable> devices=iothub.GetDevices();

            Console.WriteLine("Simulated Device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceName, deviceKey));
            iothub.SendDeviceToCloudMessagesAsync(deviceName,deviceClient);
            Console.ReadLine();
        }
    }
}
