using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Data;


namespace WebSite
{
    public class BasePage : System.Web.UI.Page
    {
        public static HMonitorData HMonitorDataContext
        {
            get
            {
                var objectContextKey = HttpContext.Current.GetHashCode().ToString("x");
                if (!HttpContext.Current.Items.Contains(objectContextKey))
                {
                    HttpContext.Current.Items.Add(objectContextKey, new HMonitorData());
                }
                return HttpContext.Current.Items[objectContextKey] as HMonitorData;
            }
        }
    }
}