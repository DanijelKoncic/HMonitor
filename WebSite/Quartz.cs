using System;
using System.Linq;
using Quartz;
using Website.Data;

namespace WebSite
{
    public class QuartzWeatherUG : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var currentWeather = new Weather();
            var error =
                currentWeather.GetCurrentWeather(
                    "http://api.wunderground.com/api/dfc7c71a78849bb0/conditions/pws:1/q/pws:IUNDEFIN41.xml");
            if (error)
            {
                //Snimi temperaturu
                currentWeather.SaveCurrentWeather("WUTEMP01", null, currentWeather.temperaturaC, currentWeather.observation_time_rfc822);
                //Snimi vlagu
                currentWeather.SaveCurrentWeather("WUHUMI01", null, currentWeather.relative_humidity, currentWeather.observation_time_rfc822);
                
            }
            else
            {
                //TODO: Prikazati grešku
            }
        }
    }
}




//public void SaveCurrentWeather(string sensorCode, string dataText, decimal dataNumeric, DateTime sampleDt)
//    {
//        //Spremi podatke u bazu prema senzoru, prikupljenoj vrijednosti i vremenu prikupljanja
//        try
//        {

//            var dc = new HMonitorData();
//            var first = dc.Sensors.First(s => s.Code == sensorCode);
//            if (first != null)
//            {
//                var sensorId = first.SensorId;

//                using (dc)
//                {
//                    var sensorHistoryData = new SensorHistoryData()
//                    {
//                        SensorId = sensorId,
//                        DataText = dataText,
//                        DataNumeric = dataNumeric,
//                        SampledDT = sampleDt,
//                        InsertedDT = DateTime.Now
//                    };
//                    dc.Add(sensorHistoryData);
//                    dc.SaveChanges();
//                }

//            }
//            else
//            {
//                //TODO: Implementirati prikaz greške "
//            }
//        }
//        catch (Exception)
//        {
//            throw;
//        }
//    }