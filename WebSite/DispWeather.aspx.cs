using System;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Website.Data;


namespace WebSite
{
    public partial class DispWeather : BasePage
    {

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

                //Snimi temperaturu
                currentWeather.SaveCurrentWeather("WUTEMP01", null, currentWeather.temperaturaC, currentWeather.observation_time_rfc822);
                //Snimi vlagu
                currentWeather.SaveCurrentWeather("WUHUMI01", null, currentWeather.relative_humidity, currentWeather.observation_time_rfc822);
                
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