<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models" %> 
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index.aspx</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
            <legend>Overall Results</legend>
            <table width="100%"> 
                <tr>
                    <th>#</th>
                    <th>Race</th>
                    <th>Date</th>           
                    <th>Results</th>           
                </tr>
            <%  int i = 1;
                int leagueRaceId = 0;
                foreach (HorseLeague.Models.Domain.LeagueRace leagueRace in (IEnumerable)ViewData["AllResults"])
                {
                    if (leagueRace.Id != leagueRaceId)
                    {
                    %>   
                        <tr>
                            <td><%=i%>.</td>
                            <td><%=Html.ActionLink(Html.Encode(leagueRace.Race.Name), "Details", new { id = leagueRace.Id })%></td>
                            <td><%=leagueRace.RaceDate.ToShortDateString()%></td>
                            <td align="center">
                                <table width="100%">
                                <%  BetTypes betType = BetTypes.Show;
                                    for (int j = 1; j < 4; j++)
                                    {
                                        switch (j)
                                        {
                                            case 1:
                                                betType = BetTypes.Win;
                                                break;
                                            case 2:
                                                betType = BetTypes.Place;
                                                break;
                                            case 3:
                                                betType = BetTypes.Show;
                                                break;
                                        }

                                        HorseLeague.Models.Domain.RaceDetailPayout curPayout = UIFunctions.GetPayout(betType, leagueRace);
                                       %>
                                        <tr>
                                            <td class="table-cell-inner-results-bet-type"><%=betType.ToString()%></td>
                                            <td class="table-cell-inner-results-name"><%=UIFunctions.FormatHorseNameForDisplay(curPayout.RaceDetail)%></td>    
                                            <td class="table-cell-inner-results-bet-amount"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.WinAmount)%></td>    
                                            <td class="table-cell-inner-results-bet-amount"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.PlaceAmount)%></td>    
                                            <td class="table-cell-inner-results-bet-amount"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.ShowAmount)%></td>    
                                        </tr>
                                    <%} %>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                            <td>
                                Scratches: <%=UIFunctions.GetScratchString(leagueRace)%>
                            </td>
                        </tr>
                        
            <% i++;
               leagueRaceId = leagueRace.Id;
                    }
                }
            %>
            </table> 
        </fieldset>



</asp:Content>
