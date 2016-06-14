using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client.Exceptions;
using Newtonsoft.Json;
using SimulatedDevice.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace SimulatedDevice
{
    public class IoTService
    {
        static RegistryManager registryManager;
        static string city = "New York";

        public IoTService()
        {
            registryManager = RegistryManager.CreateFromConnectionString(ConfigurationManager.AppSettings["IotConnectionString"]);
        }

        public async Task<string> AddDeviceAsync(string deviceName)
        {
            
            Device device = await registryManager.GetDeviceAsync(deviceName);
            var dev = registryManager.GetDevicesAsync(5);

            if (device == null)
            {
                try
                {
                    device = await registryManager.AddDeviceAsync(new Device(deviceName));
                }
                catch (DeviceAlreadyExistsException)
                {
                    device = await registryManager.GetDeviceAsync(deviceName);
                }

            }
            Console.WriteLine("Device key is: {0}", device.Authentication.SymmetricKey.PrimaryKey);
            return device.Authentication.SymmetricKey.PrimaryKey;
        }

        public async void SendDeviceToCloudMessagesAsync(string deviceName,DeviceClient deviceClient)
        {
            Sensor S = new Sensor();
            while (true)
            {
                var telemetryDataPoint = S.Generate(deviceName, city);
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Thread.Sleep(1000);
            }
        }

        public async Task<IEnumerable> GetDevices()
        {       
            List<Device> list= new List<Device>(await registryManager.GetDevicesAsync(1));
            return list;
        }
    }
}
