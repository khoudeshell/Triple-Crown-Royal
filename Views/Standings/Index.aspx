<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models" %> 
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
            <legend>User Standings</legend>
            <table width="100%"> 
                <tr>
                    <th rowspan="2">#</th>
                    <th rowspan="2">User</th>           
                    <th rowspan="2">Total</th>  
                    <th rowspan="2">%<br />ROI</th>    
                    <th rowspan="2">%<br />Fav</th>
                    <th colspan="6">Average Payout ($)</th>
                    <th colspan="6">Percent in Payout (%)</th>               
                </tr>
                <tr>
                    <th>WW</th>           
                    <th>WP</th>  
                    <th>WS</th>  
                    <th>PP</th>  
                    <th>PS</th>  
                    <th>SS</th>
                    <th>WW</th>
                    <th>WP</th>
                    <th>WS</th>
                    <th>PP</th>
                    <th>PS</th>
                    <th>SS</th>                              
                </tr>
            <%  int i = 1;
                DateTime lastUpdateDate = DateTime.MinValue;
                
                foreach (UserStanding userStanding in (IEnumerable)ViewData["UserStandings"])
                    { %>    
                    <tr>
                        <td><%=i%>.</td>
                        <td><%=Html.ActionLink(userStanding.aspnet_User.UserName, "Details", new { id = userStanding.UserId })%></td>
                        <td align="center"><%=userStanding.Total%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.ROI)%></td>  
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.WinFavPct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.WinWinAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.WinPlaceAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.WinShowAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.PlacePlaceAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.PlaceShowAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportAverage(userStanding.ShowShowAvg)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.WinWinPct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.WinPlacePct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.WinShowPct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.PlacePlacePct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.PlaceShowPct)%></td>
                        <td align="center"><%=UIFunctions.FormatReportPercent(userStanding.ShowShowPct)%></td>
                    </tr>
            <%          if( i == 1)
                        {
                            lastUpdateDate = userStanding.UpdateDate;
                        }   
                        i++;     
                    }
              %>
                <tr>
                    <td colspan="3" align="right">As of <%=lastUpdateDate %></td>
                </tr>
            </table> 
        </fieldset>

</asp:Content>
