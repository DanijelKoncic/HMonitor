<%@ Page Title="" Language="C#" MasterPageFile="~/Foundation.Master" AutoEventWireup="true" CodeBehind="GraphWeather.aspx.cs" Inherits="WebSite.GraphWeather" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.OpenAccess.Web" Assembly="Telerik.OpenAccess.Web.40, Version=2013.3.1211.3, Culture=neutral, PublicKeyToken=7ce17eeaf1d59342" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script src="Scripts/Highcharts-3.0.9/js/themes/dark-blue.js"></script>  --%>  
    
    <script type="text/javascript">
        $(function () {
            $("[id$='_txtDate']").datepicker({
                    showButtonPanel: true,
                    dateFormat: "dd.mm.yy",
                    beforeShow: function ()
                    {
                        $(".ui-datepicker").css('font-size', 12);
                    }
                });
        });

        $(function() {
            $("[id$='datepicker_container']").datepicker({
                showButtonPanel: true,
                dateFormat: "dd.mm.yy"
                //beforeShow: function() {
                //    $(".ui-datepicker").css('font-size', 12);
                //}
            });
        });

        <%--        $("#'<%=txtName.ClientID %>'");   
            $('#<%= DateTextBox.ClientID %>').attachDatepicker(); 
        --%> 

    </script>    
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--<div style="margin-top: 15px;">
        <script src="/Scripts/jquery.js"></script>
    
    <div class="row">
    <div class="large-8 columns">
        
        <div class="panel radius" style="background-color: #FFFFFF;">
            <h4>Grafički prikaz temperature</h4>
            <br/>

            <asp:Chart ID="Chart1" runat="server" DataSourceID="OpenAccessLinqDataSource1" Palette="Bright" BorderlineWidth="2" Height="200px" Width="1024px">
                <Series>
                    <asp:Series Name="Temperatura" 
                        ChartType="StepLine" 
                        XValueMember="SampledDT" 
                        YValueMembers="DataNumeric"
                        Legend="Legend1"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX IntervalType="Hours">
                            <MajorGrid Enabled="True" Interval="60" IntervalType="Minutes" LineColor="LightGray" />
                        <LabelStyle Format="HH:mm tt" Interval="60" IntervalType="Minutes"></LabelStyle>
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Legend1" Title="Legenda" Docking="Bottom">
                        <Position Height="13.7123747" Width="11.1436949" X="85.85631" Y="3" />
                    </asp:Legend>
                </Legends>
            </asp:Chart>

            <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="Website.Data.HMonitorData" EntityTypeName="" ResourceSetName="SensorHistoryDatas" Select="new (DataNumeric, SampledDT, SensorId)" Where="SensorId == @SensorId" OnSelecting="OpenAccessLinqDataSource1_Selecting">
                <whereparameters>
                    <asp:Parameter DefaultValue="1" Name="SensorId" Type="Int32" />
                </whereparameters>
            </telerik:OpenAccessLinqDataSource>
        </div>

    </div>
-->
    <div class="row">
        <div class="large-10 columns">
            <h4 style="color:black">Grafički prikaz temperature</h4>  

        </div>
        <div class="large-2 columns">
            <asp:TextBox ID="TextBox1" runat="server" Text="Odaberi datum" AutoPostBack="True" OnTextChanged="txtDate_TextChanged" />
        </div>
        <hr/>
    </div>        
    <div class="row">
        <div class="large-9 columns">
        
            <div class="panel radius" style="background-color: #FFFFFF;">
                <asp:Literal runat="server" ID="ltrChart"></asp:Literal>
            </div>

        </div>

        <div class="large-3 columns">
            <div class="row">
                <div class="large-12 columns">
                    <asp:Label ID="txtLabel" runat="server" Text="Datum izvještaja" AssociatedControlID="txtDate"></asp:Label>
                </div>
                <div class="large-12 columns">
                    <asp:TextBox ID="txtDate" runat="server" Text="Odaberi datum" AutoPostBack="True" OnTextChanged="txtDate_TextChanged" />    
                </div>
                <div id="datepicker_container"class="large-12 columns">
                    
                </div>
            </div>
        </div>

    </div>

    
    <!--
    <div class="row">
        
        
        

        
        
   <div class="large-4 columns">
        <div class="panel">
           
            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged">
            </asp:Calendar>
            <div class="text-right">
            </div>
        </div>
    </div>

    </div>
        -->
        
    

</asp:Content>
