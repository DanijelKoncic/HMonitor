<%@ Page Title="" Language="C#" MasterPageFile="~/Foundation.Master" AutoEventWireup="true" CodeBehind="GraphWeather1.aspx.cs" Inherits="WebSite.GraphWeather1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script src="Scripts/Highcharts-3.0.9/js/themes/dark-blue.js"></script>  --%>  
    
    <script type="text/javascript">
        //$(function () {
        //    $("[id$='_txtDate']").datepicker({
        //        showButtonPanel: true,
        //        dateFormat: "dd.mm.yy",
        //        beforeShow: function ()
        //        {
        //            $(".ui-datepicker").css('font-size', 12);
        //        }
        //    });
        //});

        $(function () {
            $("[id$='_TextBox1']").datepicker({
                showButtonPanel: true,
                dateFormat: "dd.mm.yy",
                beforeShow: function () {
                    $(".ui-datepicker").css('font-size', 10);
                }
            });
        });


        //$(function() {
        //    $("[id$='datepicker_container']").datepicker({
        //        showButtonPanel: true,
        //        dateFormat: "dd.mm.yy"
                //beforeShow: function() {
                //    $(".ui-datepicker").css('font-size', 12);
                //}
        //    });
        //});

                <%--        $("#'<%=txtName.ClientID %>'");   
            $('#<%= DateTextBox.ClientID %>').attachDatepicker(); 
        --%> 







    </script>    
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >  
        <ContentTemplate>     
    
            <div class="row">
                <div class="large-10 columns">
                    <h4 style="color:black">Grafički prikaz temperature</h4>  

                </div>
                <div class="large-2 columns">
                    <asp:TextBox ID="TextBox1" runat="server" Text="Odaberi datum" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" />
                </div>
                <hr/>
            </div>
            
            <div class="row">
                <div class="large-12 columns">
                    <div class="panel radius" style="background-color: #FFFFFF;">
                        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
                        <div id="chart_container_new"></div>                        
                        
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>

<%--                <div class="large-3 columns">
                    <div class="row">
                        <div id="datepicker_container" class="large-12 columns">
                        </div>
                        
                        <div class="large-12 columns">
                            <asp:Label ID="txtLabel" runat="server" Text="Datum izvještaja" AssociatedControlID="txtDate"></asp:Label>
                        </div>
                        
                        <div class="large-12 columns">
                            <asp:TextBox ID="txtDate" runat="server" Text="Odaberi datum" OnTextChanged="txtDate_TextChanged" AutoPostBack="True" />    
                        </div>
                        
                        <div class="large-12 columns">
                            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                        </div>   
                    </div>
                </div>--%>
            </div>
        
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Content/Images/ajax_loader-2.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </ProgressTemplate>
    </asp:UpdateProgress>

    <script type="text/javascript">
 
        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            window.nacrtajgraf();
        });
    
        //prm.add_endRequest(function () {
        //    $(function () {
        //        $("[id$='_txtDate']").datepicker({
        //            showButtonPanel: true,
        //            dateFormat: "dd.mm.yy",
        //            beforeShow: function () {
        //                $(".ui-datepicker").css('font-size', 12);
        //            }
        //        });
        //    });
        //});
        
        prm.add_endRequest(function () {
            $(function () {
                $("[id$='_TextBox1']").datepicker({
                    showButtonPanel: true,
                    dateFormat: "dd.mm.yy",
                    beforeShow: function () {
                        $(".ui-datepicker").css('font-size', 10);
                    }
                });
            });
        });
    
    </script>
</asp:Content>
