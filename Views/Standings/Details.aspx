<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models.Domain" %> 
<%@ Import Namespace="HorseLeague.Models" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Details</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <%
            UserLeague userLeague = (UserLeague)this.ViewData["UserLeague"];
             %>
        <legend>Results for <%=userLeague.User.UserName%></legend>
            <table width="100%"> 
                <tr>
                    <th>#</th>
                    <th>Race</th>
                    <th>Date</th>           
                    <th>Results</th>
                    <th>Weight</th>
                    <th>Total</th>           
                </tr>
            <%  int i = 1;
                int leagueRaceId = 0;
                double runningTotal = 0;
                double raceTotal = 0;
                Calculator payoutCalc = null;
                BetTypes betType = BetTypes.Show;
                                    
                foreach (LeagueRace leagueRace in (IEnumerable)ViewData["UserRaceResults"])
                {
                    if (leagueRace.Id != leagueRaceId && leagueRace.RaceDetailPayouts.Count > 0)
                    {
                        raceTotal = 0;
                   %>   
                        <tr>
                            <td><%=i%>.</td>
                            <td><%=Html.Encode(leagueRace.Race.Name)%></td>
                            <td><%=leagueRace.RaceDate.ToShortDateString()%></td>
                            <td align="center">
                                <table width="100%">
                                <%  for (int j = 1; j < 4; j++)
                                    {
                                        switch (j)
                                        {
                                            case 1:
                                                betType = BetTypes.Win;
                                                payoutCalc = new WinCalculator();
                                                break;
                                            case 2:
                                                betType = BetTypes.Place;
                                                payoutCalc = new PlaceCalculator();
                                                break;
                                            case 3:
                                                betType = BetTypes.Show;
                                                payoutCalc = new ShowCalculator();
                                                break;
                                        }
                                        UserRaceDetail userSelection = userLeague.GetUserPickByType(leagueRace, betType);
                                        RaceDetailPayout payout = userSelection.RaceDetail.GetRaceDetailPayout();

                                        payoutCalc.Payout = payout;
                                        
                                        %>
                                        <tr>
                                            <td><%=betType.ToString()%></td>
                                            <td><%=UIFunctions.FormatHorseNameForDisplay(userSelection.RaceDetail.PostPosition, userSelection.RaceDetail.Horse.Name)%></td>    
                                            <%
                                                if (payout == null)
                                                {
                                                %>
                                                    <td>-</td>    
                                                    <td>-</td>    
                                                    <td>-</td>
                                            <%
                                                }
                                                else
                                                {%>                                        
                                                    <td><%=UIFunctions.GetBetTypeValueFromPayout(payoutCalc.WinAmount)%></td>    
                                                    <td><%=UIFunctions.GetBetTypeValueFromPayout(payoutCalc.PlaceAmount)%></td>    
                                                    <td><%=UIFunctions.GetBetTypeValueFromPayout(payoutCalc.ShowAmount)%></td>    
                                                <%
                                                    raceTotal += payoutCalc.Total;
                                                } %>
                                        </tr>
                                    <%} %>
                                </table>
                            </td>
                            <td><%=leagueRace.Weight%></td>
                            <td><%=raceTotal%></td>
                        </tr>
                        
            <% i++;
               leagueRaceId = leagueRace.Id;
               runningTotal += raceTotal;         
                    }
                }
            %>
            <tr>
                <td colspan="5" align="right">&nbsp;</td>
                <td><b><%=runningTotal%></b></td>
            </tr>
            </table> 
        </fieldset>
        
        <div>
            <%=Html.ActionLink("Back to List", "Index") %>
        </div>
</asp:Content>
