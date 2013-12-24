using Quartz;

namespace WebSite
{
    //public class Quartz : IJob
    //{
    //    private static ILog _log = LogManager.GetLogger(typeof(Quartz));
        
    //    public Quartz()
    //    {
    //    }

    //    public virtual void Execute(IJobExecutionContext context)
    //    {
    //        _log.Info(string.Format("Hello world! - {0}", System.DateTime.Now.ToString("r")));
    //    }
    //}

    //public interface IJob
    //{
    //    void Execute(IJobExecutionContext context);
    //}


    public class QuartzWeatherUG : IJob
    {
        public QuartzWeatherUG ()
        {
        }

        public void Execute(IJobExecutionContext context)
        {
            var currentWeather = new Weather();
            var error =
                currentWeather.GetCurrentWeather(
                    "http://api.wunderground.com/api/dfc7c71a78849bb0/conditions/pws:1/q/pws:IUNDEFIN41.xml");
            if (error)
            {
                //Snimi temperaturu
                currentWeather.SaveCurrentWeather((int)Weather.EnumSensor.Temperatura,null,currentWeather.temperaturaC,currentWeather.observation_time_rfc822);
                //Snimi vlagu
                currentWeather.SaveCurrentWeather((int)Weather.EnumSensor.Relativna_vlaga,null,currentWeather.relative_humidity,currentWeather.observation_time_rfc822);
                
            }
        }
    }

}