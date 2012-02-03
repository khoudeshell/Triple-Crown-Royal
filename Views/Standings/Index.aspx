<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.Domain.League>" %>
<%@ Import Namespace="HorseLeague.Models" %> 
<%@ Import Namespace="HorseLeague.Models.Domain" %> 
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
            <legend>User Standings</legend>
            <table width="100%" id="standings"> 
                <tr>
                    <th rowspan="2" title="Current position">#</th>
                    <th rowspan="2" title="Change since last week">Chg</th>
                    <th rowspan="2">User</th>           
                    <th rowspan="2">Total</th>  
                    <th rowspan="2" title="Return on Investment...If the picks would have been bet at the track">%<br />ROI</th>    
                    <th rowspan="2" title="Percent of races that the favorite was picked for the winner">%<br />Fav</th>
                    <th colspan="6" title="Average payouts based for the 6 types of bets">Average Payout ($)</th>
                    <th colspan="6" title="Percent of the time the bet type paid out">Percent in Payout (%)</th>               
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
                
                foreach (HorseLeague.Models.Domain.UserStandings userStanding in this.Model.UserStandings.OrderByDescending(x => x.Total))
                    { %>    
                    <tr>
                        <td><%=i%>.</td>
                        <td><span class="<%=UIFunctions.GetStandingDeltaClass(userStanding) %>"><%=UIFunctions.GetStandingDelta(userStanding)%></span></td>
                        <td><%=Html.ActionLink(userStanding.UserLeague.User.UserName, "Details", new { id = userStanding.UserLeague.User.Id })%></td>
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
        <br />
        
         <fieldset>
            <legend>Exacta Standings</legend>

            <table width="100%">
                <tr>
                    <th>#</th>
                    <th>User</th>
                    <th>Amount</th>
                    <th>Race</th>
                </tr>

                <%
                    IList<UserRaceExoticPayout> exactaPayouts = this.Model.GetExactaPayouts();
                    int j=1;

                    if (exactaPayouts.Count == 0)
                    {
                 %>
                        <tr>
                            <td colspan="4">No exacta winners</td>
                        </tr>
                 <%
                    }
                    else
                    {
                        foreach (UserRaceExoticPayout exactaPayout in exactaPayouts)
                        {
                 %>
                        <tr>
                            <td><%=j%></td>
                            <td><%=exactaPayout.UserLeague.User.UserName%></td>
                            <td><%=UIFunctions.FormatReportAverage(exactaPayout.RaceExoticPayout.Amount)%></td>
                            <td><%=exactaPayout.RaceExoticPayout.LeagueRace.Race.Name%></td>
                        </tr>
                 <%
                     j++;
                        }
                    }    
                 %>
            </table>
        </fieldset>
        <br />
         <fieldset>
            <legend>Trifecta Standings</legend>

            <table width="100%"> 
                <tr>
                    <th>#</th>
                    <th>User</th>
                    <th>Amount</th>
                    <th>Race</th>
                </tr>

                <%
                    IList<UserRaceExoticPayout> trifectaPayouts = this.Model.GetTrifectaPayouts();
                    int k=1;

                    if (trifectaPayouts.Count == 0)
                    {
                 %>
                        <tr>
                            <td colspan="4">No trifecta winners</td>
                        </tr>
                 <%
                    }
                    else
                    {
                        foreach (UserRaceExoticPayout trifectaPayout in trifectaPayouts)
                        {
                 %>
                        <tr>
                            <td><%=k%></td>
                            <td><%=trifectaPayout.UserLeague.User.UserName%></td>
                            <td><%=trifectaPayout.RaceExoticPayout.Amount%></td>
                            <td><%=trifectaPayout.RaceExoticPayout.LeagueRace.Race.Name%></td>
                        </tr>
                 <%
                     k++;
                        }
                    }  
                 %>
            </table>
        </fieldset>
</asp:Content>
