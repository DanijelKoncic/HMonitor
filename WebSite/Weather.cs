using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using WebSite.Data;

namespace WebSite
{
    public class Weather
    {
        public string lokacija { get; set; }
        public string temperaturaC { get; set; }
        public string observation_time_rfc822 { get; set; }
        //public string local_time_rfc822 { get; set; }
        //public string weather { get; set; }
        public string relative_humidity { get; set; }
        //public string wind_string { get; set; }
        //public string wind_dir { get; set; }
        //public string wind_degrees { get; set; }
        //public string wind_kph { get; set; }
        //public string wind_gust_kph { get; set; }
        //public string pressure_mb { get; set; }
        //public string pressure_trend { get; set; }
        //public string dewpoint_c { get; set; }
        //public string feelslike_c { get; set; }
        //public string visibility_km { get; set; }
        //public string solarradiation { get; set; }
        //public string UV { get; set; }
        //public string precip_1hr_metric { get; set; }
        //public string precip_today_metric { get; set; }
        public string icon_url { get; set; }

        public enum EnumSensor
        {
            Temperatura = 1,
            Relativna_vlaga
            
            //WUTemperature,
            //WULocation,
            //WURelativeHumidity,
            //WUWindString,
            //WUWindDir,
            //WUWindDegrees,
            //WUWindKph,
            //WUWindGustKph,
            //WUPressureMb,
            //WUPressureTrend,
            //WUDewpointC,
            //WUFeelslikeC,
            //WUVisibilityKm,
            //WUSolarradiation,
            //WUUV,
            //WUPrecip_1HrMetric,
            //WUPrecipTodayMetric,
            //WUIconUrl,
        }

        public bool GetCurrentWeather(string weatherQuery)
        {
            try
            {
                //dohvati podatke sa webservera
                var weatherData = new XmlDocument();

                weatherData.Load(weatherQuery);
                var xmlNode = weatherData.GetElementsByTagName("icon_url").Item(0);
                if (xmlNode != null)
                    icon_url = xmlNode.InnerText;

                xmlNode = weatherData.GetElementsByTagName("temp_c").Item(0);
                if (xmlNode != null)
                    temperaturaC = xmlNode.InnerText;

                xmlNode = weatherData.GetElementsByTagName("relative_humidity").Item(0);
                if (xmlNode != null)
                    relative_humidity = xmlNode.InnerText;
                    relative_humidity = relative_humidity.Replace("%", String.Empty);

                xmlNode = weatherData.GetElementsByTagName("observation_time_rfc822").Item(0);
                if (xmlNode != null)
                    observation_time_rfc822 = xmlNode.InnerText;

                xmlNode = weatherData.GetElementsByTagName("full").Item(0);
                if (xmlNode != null)
                    lokacija = xmlNode.InnerText;

                return true;
            }
            catch (Exception)
            {
                //return 0;
                throw;
            }
        }

        public void SaveCurrentWeather (int _sensor, string _dataText, decimal _dataNumeric, DateTime _sampleDT)
        {
            //Spremi podatke u bazu
            try
            {
                using (var entityModel = new HistoricalDBEntities())
                {
                    var sensorHistoryData = new SensorHistoryData()
                    {
                        SensorId = _sensor,
                        DataText = _dataText,
                        DataNumeric = _dataNumeric,
                        SampledDT = Convert.ToDateTime(_sampleDT),
                        InsertedDT = DateTime.Now,
                    };
                    entityModel.SensorHistoryData.Add(sensorHistoryData);
                    entityModel.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}



//var a = weatherData.GetElementsByTagName("full").Count;
//var b = weatherData.GetElementsByTagName("full").Item(0).InnerText;
//icon_url = (weatherData.GetElementsByTagName("icon_url").Count > 0) ? weatherData.GetElementsByTagName("icon_url").Item(0).InnerText : string.Empty;
//lokacija = (weatherData.GetElementsByTagName("full").Count > 0) ? weatherData.GetElementsByTagName("full").Item(0).InnerText  : string.Empty;
//XmlNode currentObservation = weatherData.SelectSingleNode("response").SelectSingleNode("current_observation");
//prenesi vrijednosti u varijable
//lokacija = currentObservation.SelectSingleNode("observation_location").SelectSingleNode("full").InnerXml;
//temperaturaC = string.Format("{0} {1}", currentObservation.SelectSingleNode("temp_c").InnerXml, " C");
//observation_time_rfc822 = currentObservation.SelectSingleNode("observation_time_rfc822").InnerXml;