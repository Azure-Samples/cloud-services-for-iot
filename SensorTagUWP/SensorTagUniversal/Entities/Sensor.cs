using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorTagUniversal.Entities
{
    public class Sensor
    {     
        public string Time;
        public string SensorName;
        public double Temperature;
        public double Humidity;
        public string City;

        public Sensor Generate(string name,double temperature,double humidity,string city)
        {
            return new Sensor
            {
                Time = DateTime.UtcNow.ToString(),
                SensorName = name,
                Temperature = temperature,
                Humidity = humidity,
                City=city
            };
        }
    }
}
