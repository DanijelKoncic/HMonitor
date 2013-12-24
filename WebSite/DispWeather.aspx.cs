using System;
using Quartz;
using Quartz.Impl;


namespace WebSite
{
    public partial class DispWeather : System.Web.UI.Page
    {
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
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GridView1.DataBind();
            }
        }

        protected void GetWeather_Click(object sender, EventArgs e)
        {
            var currentWeather = new Weather();
            var error = currentWeather.GetCurrentWeather("http://api.wunderground.com/api/dfc7c71a78849bb0/conditions/pws:1/q/pws:IUNDEFIN41.xml");

            if (error)
            {
                weatherIcon.ImageUrl = currentWeather.icon_url;
                lblLokacija.Text = string.Format("{0} {1}", "Lokacija:", currentWeather.lokacija);
                lblTemperaturaC.Text = string.Format("{0} {1}", "Trenutna temperatura (C):", currentWeather.temperaturaC);

                //spremi temperaturu i vlagu
                currentWeather.SaveCurrentWeather((int)EnumSensor.Temperatura, null, currentWeather.temperaturaC, currentWeather.observation_time_rfc822);
                currentWeather.SaveCurrentWeather((int)EnumSensor.Relativna_vlaga, null, currentWeather.relative_humidity, currentWeather.observation_time_rfc822);
                
                GridView1.DataBind();
                //var proba = Convert.ToDateTime(currentWeather.observation_time_rfc822);
            }
            else
            {
                Response.Write("There has been an error fetching weather data.");
            }
        }
    }
}



















//using (var model = new HystoricalDBEntities())
//{
//    foreach (var hisda in model.HistoryData)
//    {
//        Response.Write(hisda.EntryDateTime);
//        Response.Write(hisda.DataText);
//    }
//}