using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace DeviceIdentityIOTHub
{
    
    class Program
    {
        static RegistryManager registryManager;
        //static string connectionString = "HostName=cspiothubigdaq6sgdrajq.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Dt9mCzzpn29cbWVglfBXak9LiN68gzetQQQDDmaMUZE=";
        static string connectionString = "HostName=cspiothubt5fmowudia5fa.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=G0Y8FSMrCVxenjd7No5mhEwVr28ohoDN45Oz3IL0B7s=";
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }
        private async static Task AddDeviceAsync()
        {
            string deviceId = "SensorTag";
            Device device;
           
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
