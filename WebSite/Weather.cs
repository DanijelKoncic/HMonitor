using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Config.Sql;
using Website.Data;

namespace WebSite
{
    public class Weather
    {
        
        public string lokacija { get; set; }
        public decimal temperaturaC { get; set; }
        public DateTime observation_time_rfc822 { get; set; }
        //public string local_time_rfc822 { get; set; }
        //public string weather { get; set; }
        public decimal relative_humidity { get; set; }
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
            //Sadrži enumeraciju pripadajućih senzora u bazi podataka
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
                    temperaturaC = Convert.ToDecimal(xmlNode.InnerText.Replace(".", ","));

                xmlNode = weatherData.GetElementsByTagName("relative_humidity").Item(0);
                if (xmlNode != null)
                    relative_humidity =  Convert.ToDecimal(xmlNode.InnerText.Replace("%", String.Empty));

                xmlNode = weatherData.GetElementsByTagName("observation_time_rfc822").Item(0);
                if (xmlNode != null)
                    observation_time_rfc822 = Convert.ToDateTime(xmlNode.InnerText);

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

        //public void SaveCurrentWeather (HMonitorData _dc, string sensorCode, string dataText, decimal dataNumeric, DateTime sampleDt)
        //{
        //    //Spremi podatke u bazu prema senzoru, prikupljenoj vrijednosti i vremenu prikupljanja
        //    try
        //    {

        //        var dc = _dc;
        //        var first = dc.Sensors.First(s => s.Code == sensorCode);
        //        if (first != null)
        //        {
        //            var sensorId = first.SensorId;

        //            using (dc)
        //            {
        //                var sensorHistoryData = new SensorHistoryData()
        //                    {
        //                        SensorId = sensorId,
        //                        DataText = dataText,
        //                        DataNumeric = dataNumeric,
        //                        SampledDT = sampleDt,
        //                        InsertedDT = DateTime.Now
        //                    };
        //                dc.Add(sensorHistoryData);
        //                dc.SaveChanges();
        //            }

        //        }
        //        else
        //        {
        //            //TODO: Implementirati prikaz greške "
        //        }


        //        //Snimanje u Entities modelu
        //        //using (var entityModel = new HistoricalDBEntities())
        //        //{
        //        //    var sensorHistoryData = new SensorHistoryData()
        //        //    {
        //        //        SensorId = sensor,
        //        //        DataText = dataText,
        //        //        DataNumeric = dataNumeric,
        //        //        SampledDT = sampleDt,
        //        //        InsertedDT = DateTime.Now,
        //        //    };
        //        //    entityModel.SensorHistoryData.Add(sensorHistoryData);
        //        //    entityModel.SaveChanges();
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}