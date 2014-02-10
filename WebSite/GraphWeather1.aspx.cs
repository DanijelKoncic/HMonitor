using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Website.Data;


namespace WebSite
{
    public partial class GraphWeather1 : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Text = DateTime.Now.AddDays(0).ToShortDateString();
                PrepareChart(Convert.ToDateTime(TextBox1.Text), "WUTEMP__01", "WUHUMIDI01");
            }
        }

        private void PrepareChart(DateTime _dateTime, string _sensorName, string _sensorName1)
        {

            var dc = HMonitorDataContext;

            //var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.InsertedDT > Calendar1.SelectedDate.Date
            //                                        && s.InsertedDT < Calendar1.SelectedDate.AddDays(1).Date
            //                                        && s.SensorId == ConvertSafe.ToInt32(1)).ToList();
            try
            {
                int sensorId = dc.Sensors.Single(s => s.Code == _sensorName).SensorId;
                int sensorId1 = dc.Sensors.Single(s => s.Code == _sensorName1).SensorId;

                //Dohvati Listu
                var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.SampledDT > _dateTime.Date
                                            && s.SampledDT < _dateTime.AddDays(1).Date
                                            && (s.SensorId == sensorId || s.SensorId == sensorId1)
                                            && (s.SampledDT != null || s.DataNumeric != null)
                                            ).OrderBy(o => o.SensorHistoryDataId).ToList();

                //Dohvati podatke iz Liste
                //s.InsertedDT.Value.ToShortTimeString()
                //var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorID).Select(g => g.SampledDT.Value.ToShortTimeString()).ToList();
                var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorId).Select(g => g.SampledDT.Value.ToShortTimeString());
                var podaci = graphListaSenzora.Where(s => s.SensorId == sensorId).Select(s => s.DataNumeric);
                var podaci1 = graphListaSenzora.Where(s => s.SensorId == sensorId1).Select(s => s.DataNumeric);

                //Kreiraj Serije
                Series temperatura = new Series();
                temperatura.Data = new Data(new object[]
                                                {
                                                    podaci.ToArray()
                                                });
                temperatura.Name = "Temperatura";
                temperatura.Type = ChartTypes.Line;
                temperatura.YAxis = "0";

                //Kreiraj Serije
                Series vlaga = new Series();
                vlaga.Data = new Data(new object[]
                                                {
                                                    podaci1.ToArray()
                                                });
                vlaga.Name = "Vlaga";
                vlaga.Type = ChartTypes.Line;
                vlaga.YAxis = "1";

                //
                var series = new Series[] { temperatura, vlaga };


                //Pripremi podatke za Chart objekt
                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                    .InitChart(new Chart { Height = 260 })
                    .SetTitle(new Title { Text = "Kretanje temperature zraka" })
                    .SetSubtitle(new Subtitle { Text = "dnevni podaci u °C" })
                    .SetXAxis(new XAxis
                    {
                        Categories = kategorije.ToArray(),
                        Title = new XAxisTitle { Text = "Vrijeme promatranja" },
                        TickInterval = 10,
                        Type = AxisTypes.Datetime,
                        TickColor = Color.BlueViolet,
                        GridLineWidth = 1
                    })
                    .SetYAxis(new YAxis[] 
                    { 
                        new YAxis
                            {
                                Title = new YAxisTitle{Text = "Temperatura"},
                                Id = "0",
                                Max = 20,
                                Min = -20
                            }, 
                        new YAxis
                            {
                                Title = new YAxisTitle{Text = "Vlaga"},
                                Opposite = true
                            }
                    })
                    .SetSeries(series)
                    //.SetTooltip(new Tooltip{ValueSuffix = "°C"})
                    .SetLegend(new Legend
                    {
                        Align = HorizontalAligns.Left,
                        VerticalAlign = VerticalAligns.Top,
                        X = 50,
                        Y = 20,
                        Floating = true,
                        BorderWidth = 0
                    });

                //TODO: Riješiti ovaj tweak
                var output = chart.ToHtmlString();
                //var output1 = output.Replace("[[", "[");
                //var output2 = output1.Replace("]]", "]");
                
                System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                sb1.Append(output);
                sb1.Replace("<div id='chart_container'></div>\r\n","");
                sb1.Replace("[[", "[");
                sb1.Replace("]]", "]");
                sb1.Replace("chart_container", "chart_container_new");
                sb1.Replace("$(document).ready(function() {", "function nacrtajgraf() {");
                sb1.Replace("<script type='text/javascript'>\r\n", "");
                sb1.Replace("</script>\r\n", "");
                sb1.Replace("\t});\r\n});\r\n", "\t});\r\n};\r\n");
                var output1 = sb1.ToString();

                ScriptManager.RegisterStartupScript(ltrChart, ltrChart.GetType(), "scriptname", output1, true);
                //ltrChart.Text = sb1.ToString();
                Label1.Text = "Refreshed at: " + DateTime.Now.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        //protected void txtDate_TextChanged(object sender, EventArgs e)
        //{
        //    PrepareChart(Convert.ToDateTime(txtDate.Text), "WUTEMP__01", "WUHUMIDI01");
        //    Label1.Text = "Refreshed at: " + DateTime.Now.ToString();
        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    PrepareChart(Convert.ToDateTime(txtDate.Text), "WUTEMP__01", "WUHUMIDI01");
        //    Label1.Text = "Refreshed at: " + DateTime.Now.ToString();
        //}

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            PrepareChart(Convert.ToDateTime(TextBox1.Text), "WUTEMP__01", "WUHUMIDI01");
            Label1.Text = "Refreshed at: " + DateTime.Now.ToString();
        }



    }


}



//UpdatePanel1.RenderControl();

//var output3 = "var chart;\r\n" +
//              "function nacrtajgraf() {\r\n\t" +
//              "chart = new Highcharts.Chart({\r\n\t\t" +
//              "chart: { renderTo:'chart_container', height: 300 }, \r\n\t\tlegend: { align: 'left', borderWidth: 0, floating: true, verticalAlign: 'top', x: 50, y: 20 }, \r\n\t\tsubtitle: { text: 'dnevni podaci u °C' }, \r\n\t\ttitle: { text: 'Kretanje temperature zraka' }, \r\n\t\txAxis: { categories: ['11:40', '11:59', '12:20', '12:39', '12:59', '13:20', '13:39', '14:00', '14:19', '14:39', '14:59', '15:19', '15:40'], gridLineWidth: 1, tickColor: 'blueviolet', tickInterval: 10, title: { text: 'Vrijeme promatranja' }, type: 'datetime' }, \r\n\t\tyAxis: [{ id: 0, max: 20, min: -20, title: { text: 'Temperatura' } }, { opposite: true, title: { text: 'Vlaga' } }], \r\n\t\tseries: [{ data: [5.50, 6.10, 6.10, 7.30, 7.90, 7.50, 6.90, 7.00, 6.70, 7.10, 6.80, 6.90, 6.50], name: 'Temperatura', type: 'line', yAxis: 0 }, { data: [98.00, 95.00, 91.00, 89.00, 84.00, 80.00, 77.00, 74.00, 73.00, 72.00, 71.00, 71.00, 71.00], name: 'Vlaga', type: 'line', yAxis: 1 }]\r\n\t" +
//              "});\r\n" +
//              "};\r\n";

//var chart;
//function nacrtajgraf() {
//    chart = new Highcharts.Chart({
//        chart: { renderTo: 'chart_container_new', height: 300 },
//        legend: { align: 'left', borderWidth: 0, floating: true, verticalAlign: 'top', x: 50, y: 20 },
//        subtitle: { text: 'dnevni podaci u °C' },
//        title: { text: 'Kretanje temperature zraka' },
//        xAxis: { categories: ['11:40', '11:59', '12:20', '12:39', '12:59', '13:20', '13:39', '14:00', '14:19', '14:39', '14:59', '15:19', '15:40'], gridLineWidth: 1, tickColor: 'blueviolet', tickInterval: 10, title: { text: 'Vrijeme promatranja' }, type: 'datetime' },
//        yAxis: [{ id: 0, max: 20, min: -20, title: { text: 'Temperatura' } }, { opposite: true, title: { text: 'Vlaga' } }],
//        series: [{ data: [5.50, 6.10, 6.10, 7.30, 7.90, 7.50, 6.90, 7.00, 6.70, 7.10, 6.80, 6.90, 6.50], name: 'Temperatura', type: 'line', yAxis: 0 }, { data: [98.00, 95.00, 91.00, 89.00, 84.00, 80.00, 77.00, 74.00, 73.00, 72.00, 71.00, 71.00, 71.00], name: 'Vlaga', type: 'line', yAxis: 1 }]
//    });
//};

//string javaScript = @"<script language=JavaScript>\n" + @"alert('Button1_Click client-side');\n" + @"</script>";
//ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "JSCR", javaScript, false);

//ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "nacrtajgraf", output3, true);

//System.Text.StringBuilder sb = new System.Text.StringBuilder();
//sb.Append(@"<script language='javascript'>");
//sb.Append(@"alert('Button1_Click client-side');");
//sb.Append(@"</script>");

//ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "JSCR", sb.ToString(),true);