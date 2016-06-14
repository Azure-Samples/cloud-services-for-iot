using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDevice.Entities
{
    public class Sensor
    {
        public string Time;
        public string SensorName;
        public double Temperature;
        public double Humidity;
        public string City;

        static Random R = new Random();
        //static string[] sensorNames = new[] { "PumpA", "PumpB", "PumpC", "PumpD", "PumpE" };
        //static string[] citylist = new[] { "Chicago", "New York", "Los Angeles", "Dallas", "San Francisco" };

        public Sensor Generate(string name,string city)
        {
            return new Sensor {
                Time = DateTime.UtcNow.ToString(),
                SensorName = name,
                Temperature = R.Next(20, 40),
                Humidity = R.Next(-5, 10),
                City=city
               };
        }
    }
}
