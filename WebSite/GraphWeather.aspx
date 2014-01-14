<%@ Page Title="" Language="C#" MasterPageFile="~/Foundation.Master" AutoEventWireup="true" CodeBehind="GraphWeather.aspx.cs" Inherits="WebSite.GraphWeather" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.OpenAccess.Web" Assembly="Telerik.OpenAccess.Web.40, Version=2013.3.1211.3, Culture=neutral, PublicKeyToken=7ce17eeaf1d59342" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div style="margin-top: 15px;">
    <div class="large-8 columns">
        
        <div class="panel radius" style="background-color: #FFFFFF;">
            <h4>Grafički prikaz temperature</h4>
            <br/>

            <asp:Chart ID="Chart1" runat="server" DataSourceID="OpenAccessLinqDataSource1" Width="1024" Palette="Bright">
                <Series>
                    <asp:Series Name="Series1" ChartType="Spline" XValueMember="SampledDT" YValueMembers="DataNumeric"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX IntervalType="Hours">
                        <LabelStyle Format="HH:mm tt"></LabelStyle>
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>

            <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="Website.Data.HMonitorData" EntityTypeName="" ResourceSetName="SensorHistoryDatas" Select="new (DataNumeric, SampledDT)" Where="SensorId == @SensorId" OnSelecting="OpenAccessLinqDataSource1_Selecting">
                <whereparameters>
                    <asp:Parameter DefaultValue="1" Name="SensorId" Type="Int32" />
                </whereparameters>
            </telerik:OpenAccessLinqDataSource>

        </div>

    </div>
    <div class="large-4 columns">
        <div class="panel">

<%--        <p>
            Sky: 
        </p>
        <p>
            <asp:Label ID="lblLokacija" runat="server" Text="Lokacija:" ></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblTemperaturaC" runat="server" Text="Trenutna temperatura (C):" ></asp:Label>
        </p>--%>
            
           
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            

        <div class="text-right">
        </div>
        </div>
    </div>
    </div>    

</asp:Content>
