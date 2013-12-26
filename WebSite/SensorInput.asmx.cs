using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebSite
{
    /// <summary>
    /// Summary description for SensorInput
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SensorInput : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public double FahrenheitToCelsius(double Fahrenheit)
        {
            return ((Fahrenheit - 32) * 5) / 9;
        }

        [WebMethod]
        public double CelsiusToFahrenheit(double Celsius)
        {
            return ((Celsius * 9) / 5) + 32;
        }

    }
}
