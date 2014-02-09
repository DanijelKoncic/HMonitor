<%@ Page Title="" Language="C#" MasterPageFile="~/Foundation.Master" AutoEventWireup="true" CodeBehind="DispWeather.aspx.cs" Inherits="WebSite.DispWeather" %>

<%@ Register Assembly="Telerik.OpenAccess.Web.40" Namespace="Telerik.OpenAccess.Web" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.OpenAccess.Web" Namespace="Telerik.OpenAccess" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row">
        <div class="large-12 columns">
            <h4 style="color:black">Prikaz prikupljenih podataka senzora</h4>  
            <br/>
        </div>
    </div>

    <div class="row">
        <div class="large-10 columns">
            <div class="panel radius" style="background-color: #FFFFFF;">
                <telerik:OpenAccessLinqDataSource ID="OpenAccessLinqDataSource1" runat="server" ContextTypeName="Website.Data.HMonitorData" EntityTypeName="" ResourceSetName="SensorHistoryDatas">
                </telerik:OpenAccessLinqDataSource>

                <asp:GridView ID="GridView1" runat="server" DataSourceID="OpenAccessLinqDataSource1" CssClass="mGrid" AutoGenerateColumns="False" DataKeyNames="SensorHistoryDataId" AllowPaging="True" PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="SensorHistoryDataId" HeaderText="SensorHistoryDataId" InsertVisible="False" ReadOnly="True" SortExpression="SensorHistoryDataId" />
                        <asp:BoundField DataField="SensorId" HeaderText="SensorId" SortExpression="SensorId" />
                        <asp:BoundField DataField="DataText" HeaderText="DataText" SortExpression="DataText" />
                        <asp:BoundField DataField="DataNumeric" HeaderText="DataNumeric" SortExpression="DataNumeric" />
                        <asp:BoundField DataField="SampledDT" HeaderText="SampledDT" SortExpression="SampledDT" />
                        <asp:BoundField DataField="InsertedDT" HeaderText="InsertedDT" SortExpression="InsertedDT" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="large-4 columns">
            <div class="panel">

                <p>
                    Sky: <asp:Image ID="weatherIcon" runat="server" Height="50px" Width="50" />
                </p>
                <p>
                    <asp:Label ID="lblLokacija" runat="server" Text="Lokacija:" ></asp:Label>
        <%--            <asp:TextBox ID="txtLokacija" runat="server"></asp:TextBox>--%>    
                </p>
                <p>
                    <asp:Label ID="lblTemperaturaC" runat="server" Text="Trenutna temperatura (C):" ></asp:Label>
        <%--            <asp:TextBox ID="txtTemperaturaC" runat="server"></asp:TextBox>--%>                
                </p>

                <div class="text-right">
                    <asp:Button ID="GetWeather" runat="server" Text="Get" OnClick="GetWeather_Click" CssClass="small secondary button"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
