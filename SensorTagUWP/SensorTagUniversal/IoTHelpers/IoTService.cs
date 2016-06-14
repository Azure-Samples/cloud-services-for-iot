using Newtonsoft.Json;
using SensorTagUniversal.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SensorTagUniversal.IoTHelpers
{
    public class IoTService
    {
        //static RegistryManager registryManager;
        //public IoTService()
        //{
        //    registryManager = RegistryManager.CreateFromConnectionString(ConfigurationManager.AppSettings["IotConnectionString"]);
        //}

        //static async Task<Boolean> addDevice(string deviceName,string iotHubName)
        //{
        //    //var authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext("https://login.microsoftonline.com/" + tenant);
        //    //var credential = new ClientCredential(clientId, clientSecret);
        //    //AuthenticationResult result = authContext.AcquireToken("https://graph.windows.net", credential);

        //    HttpClient http = new HttpClient();
        //    //var queryString = HttpUtility.ParseQueryString(string.Empty);
        //  //  queryString["api-version"] = "2016-02-03";
        //    var uri = iotHubName + ".azure-devices.net/devices/" + deviceName + "?2016-02-03";

        //    var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
        //    httpWebRequest.ContentType = "application/json";
        //    httpWebRequest.Method = "PUT";
        //    httpWebRequest.Headers.Add("Authorization", "Bearer " + result.AccessToken);

        //    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        //    {
        //        string json = "{\"url\": \"https://graph.windows.net/" + tenant + "/directoryObjects/" + userId + "\"}";
        //        streamWriter.Write(json);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }

        //    var httpResponse = httpWebRequest.GetResponseAsync().ConfigureAwait(false);

        //    return true;
        //}

        //public async Task<string> AddDeviceAsync()
        //{
        //    string deviceId = "myFirstDevice1";
        //    Device device = await registryManager.GetDeviceAsync(deviceId);
        //    var dev = registryManager.GetDevicesAsync(5);

        //    if (device == null)
        //    {
        //        try
        //        {
        //            device = await registryManager.AddDeviceAsync(new Device(deviceId));
        //        }
        //        catch (DeviceAlreadyExistsException)
        //        {
        //            device = await registryManager.GetDeviceAsync(deviceId);
        //        }

        //    }
        //    Console.WriteLine("Device key is: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        //    return device.Authentication.SymmetricKey.PrimaryKey;
        //}

        //public async void SendDeviceToCloudMessagesAsync(DeviceClient deviceClient)
        //{
        //    Sensor S = new Sensor();
        //    while (true)
        //    {
        //        var telemetryDataPoint = S.Generate();
        //        var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
        //        var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
        //        await deviceClient.SendEventAsync(message);
        //        Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

        //        Thread.Sleep(1000);
        //    }
        //}

        //public async Task<IEnumerable> GetDevices()
        //{
        //    List<Device> list = new List<Device>(await registryManager.GetDevicesAsync(1));
        //    return list;
        //}
    }
}
