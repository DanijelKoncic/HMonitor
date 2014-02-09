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
    public partial class GraphWeather : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.AddDays(0).ToShortDateString();
                //Calendar1.SelectedDate = DateTime.Now;
                //PrepareChart(DateTime.Now.AddDays(-1), "WUTEMP__01");
                //PrepareChart(DateTime.Now.AddDays(-7), "WUTEMP__01", "WUHUMIDI01");
                //--> PrepareChart1(DateTime.Now.AddDays(0), "WUTEMP__01", "WUHUMIDI01");
                //PrepareChart();

            }
        }



        private void PrepareChart(DateTime _dateTime, string _sensorName)
        {

            var dc = HMonitorDataContext;

            //var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.InsertedDT > Calendar1.SelectedDate.Date
            //                                        && s.InsertedDT < Calendar1.SelectedDate.AddDays(1).Date
            //                                        && s.SensorId == ConvertSafe.ToInt32(1)).ToList();
            try
            {
                int sensorID = dc.Sensors.Single(s => s.Code == _sensorName).SensorId;


                var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.InsertedDT > _dateTime.Date
                                            && s.InsertedDT < _dateTime.AddDays(1).Date
                                            && s.SensorId == sensorID).OrderBy(o => o.SensorId)
                                            .ToList();
                
                //Parsaj podatke i pripremi za graf
                var kategorije = new List<string> { };
                var podaci = new List<object> { };
                //var podaci1 = new List<object> { };

                foreach (var s in graphListaSenzora)
                {
                    if (s.InsertedDT != null && s.DataNumeric != null)
                    {
                        kategorije.Add(s.InsertedDT.Value.ToShortTimeString());
                        podaci.Add(s.DataNumeric);
                    }


                }

                //Pripremi podatke za Chart objekt
                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                    .InitChart(new Chart { Height = 300 })
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
                    .SetSeries(new Series() {
                                                Data = new Data(data: podaci.ToArray()),
                                                Name = "Temperatura"
                                            })
                    .SetLegend(new Legend
                    {
                        Align = HorizontalAligns.Left,
                        VerticalAlign = VerticalAligns.Top,
                        X = 50,
                        Y = 20,
                        Floating = true,
                        BorderWidth = 0
                    });

                ltrChart.Text = chart.ToHtmlString();

            }
            catch (Exception)
            {
                throw;
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
                int sensorID = dc.Sensors.Single(s => s.Code == _sensorName).SensorId;
                int sensorID1 = dc.Sensors.Single(s => s.Code == _sensorName1).SensorId;

                //Dohvati Listu
                var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.SampledDT > _dateTime.Date
                                            && s.SampledDT < _dateTime.AddDays(1).Date
                                            && (s.SensorId == sensorID || s.SensorId == sensorID1)
                                            && (s.SampledDT != null || s.DataNumeric != null)
                                            ).OrderBy(o => o.SensorHistoryDataId).ToList();

                //Dohvati podatke iz Liste
                //s.InsertedDT.Value.ToShortTimeString()
                //var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorID).Select(g => g.SampledDT.Value.ToShortTimeString()).ToList();
                var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorID).Select(g => g.SampledDT.Value.ToShortTimeString());
                var podaci = graphListaSenzora.Where(s => s.SensorId == sensorID).Select(s => s.DataNumeric ?? null).ToList();
                var podaci1 = graphListaSenzora.Where(s => s.SensorId == sensorID1).Select(s => s.DataNumeric ?? null).ToList();

                //Kreiraj Serije
                Series temperatura = new Series();
                temperatura.Data = new Data(new object[]
                                                {
                                                    podaci.ToArray()
                                                });
                temperatura.Name = "Temperatura";
                temperatura.Type = ChartTypes.Line;

                //Kreiraj Serije
                Series vlaga = new Series();
                vlaga.Data = new Data(new object[]
                                                {
                                                    podaci1.ToArray()
                                                });
                vlaga.Name = "Vlaga";
                vlaga.Type = ChartTypes.Line;

                //
                Series[] series = new Series[] { temperatura, vlaga};

                //Pripremi podatke za Chart objekt
                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
                    .InitChart(new Chart { Height = 300 })
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
                    .SetSeries(series)
                    .SetLegend(new Legend
                    {
                        Align = HorizontalAligns.Left,
                        VerticalAlign = VerticalAligns.Top,
                        X = 50,
                        Y = 20,
                        Floating = true,
                        BorderWidth = 0
                    });

                ltrChart.Text = chart.ToHtmlString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PrepareChart1(DateTime _dateTime, string _sensorName, string _sensorName1)
        {

            var dc = HMonitorDataContext;

            //var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.InsertedDT > Calendar1.SelectedDate.Date
            //                                        && s.InsertedDT < Calendar1.SelectedDate.AddDays(1).Date
            //                                        && s.SensorId == ConvertSafe.ToInt32(1)).ToList();
            try
            {
                int sensorID = dc.Sensors.Single(s => s.Code == _sensorName).SensorId;
                int sensorID1 = dc.Sensors.Single(s => s.Code == _sensorName1).SensorId;

                //Dohvati Listu
                var graphListaSenzora = dc.SensorHistoryDatas.Where(s => s.SampledDT > _dateTime.Date
                                            && s.SampledDT < _dateTime.AddDays(1).Date
                                            && (s.SensorId == sensorID || s.SensorId == sensorID1)
                                            && (s.SampledDT != null || s.DataNumeric != null)
                                            ).OrderBy(o => o.SensorHistoryDataId).ToList();

                //Dohvati podatke iz Liste
                //s.InsertedDT.Value.ToShortTimeString()
                //var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorID).Select(g => g.SampledDT.Value.ToShortTimeString()).ToList();
                var kategorije = graphListaSenzora.Where(g => g.SensorId == sensorID).Select(g => g.SampledDT.Value.ToShortTimeString());
                var podaci = graphListaSenzora.Where(s => s.SensorId == sensorID).Select(s => s.DataNumeric);
                var podaci1 = graphListaSenzora.Where(s => s.SensorId == sensorID1).Select(s => s.DataNumeric);

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
                    .InitChart(new Chart { Height = 300 })
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
                var output1 = output.Replace("[[", "[");
                var output2 = output1.Replace("]]", "]");
                //ltrChart.Text = output2;

            }
            catch (Exception)
            {
                throw;
            }
        }



        private void PrepareChart()
        {
            Series seriesRainfall2010 = new Series();
            seriesRainfall2010.Data = new Data(new object[]
                               {
                                   29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1,
                                   95.6, 54.4
                               });

            seriesRainfall2010.Name = "2010";
            seriesRainfall2010.Type = ChartTypes.Column;

            Series seriesRainfall2011 = new Series();
            seriesRainfall2011.Data = new Data(new object[]
                               {
                                   32.9, 55.5, 46.4, 321.2, 150.0, 150.0, 140.6, 160.5, 240.4, 192.1,
                                   70.6, 23.4
                               });

            seriesRainfall2011.Name = "2011";
            seriesRainfall2011.Type = ChartTypes.Column;


            Series[] series = new Series[] { seriesRainfall2010, seriesRainfall2011 };

            Highcharts chart = new Highcharts("bar")
                .SetTitle(new Title
                {
                    Text = "Rainfall in India"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Year 2010 and 2011"
                })
                .SetXAxis(new XAxis
                {
                    Categories =
                        new []
                                  {
                                      "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov",
                                      "Dec"
                                  }
                })
                .SetSeries(series);



            ltrChart.Text = chart.ToHtmlString();

            
        }

        protected void OpenAccessLinqDataSource1_Selecting(object sender, Telerik.OpenAccess.Web.OpenAccessLinqDataSourceSelectEventArgs e)
        {
        //    var selectedDate = Calendar1.SelectedDate;

        //    var a = selectedDate.Date;
        //    var b = selectedDate.AddDays(1).Date;

        //    var dc = HMonitorDataContext;
        //    var lista = dc.SensorHistoryDatas.Where(s => s.InsertedDT > selectedDate.Date 
        //                                                && s.InsertedDT < selectedDate.AddDays(1).Date).ToList();
        //    e.Result = lista;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //Chart1.DataBind();
            //PrepareChart();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string dt = Request.Form[txtDate.UniqueID];
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            PrepareChart1(Convert.ToDateTime(txtDate.Text), "WUTEMP__01", "WUHUMIDI01");
        }
    }
}



//graphListaSenzora.Select(s => new List<string>
//{s.InsertedDT.ToString()}
//).ToArray();

//dc.SensorHistoryDatas.Where(s => s.InsertedDT > Calendar1.SelectedDate.Date && s.InsertedDT < Calendar1.SelectedDate.AddDays(1).Date).ToArray()   


//protected void Page_Load(object sender, EventArgs e)
//{
//    DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
//        .SetXAxis(new XAxis
//                    {
//                        Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
//                    })
//        .SetSeries(new Series
//                    {
//                        Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
//                    });

//    ltrChart.Text = chart.ToHtmlString();
//}