<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.LeagueRaceReport>" %>
<%@ Import Namespace="HorseLeague.Models" %> 
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Details</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        LeagueRaceDomain lr = (LeagueRaceDomain)this.ViewData["LeagueRaceDomain"];
        RaceDetail favorite = lr.Favorite;
    %>
    <h2>Race Details</h2>

    <%for (int i = 1; i < 4; i++)
      {
          IList<ReportLeagueRaceBet> curReport = this.Model.GetListByType(UIFunctions.GetBetType(i));
    %>
        <fieldset>
            <legend><%=UIFunctions.GetBetType(i) %> Bets</legend>
            
            <table width="100%"> 
                <tr>
                    <th>Post</th>
                    <th>Horse</th>
                    <th>Race<br />Result</th>           
                    <th>League<br />Bet Count</th>
                    <th rowspan="<%=curReport.Count + 1%>" valign="bottom">
                        <img alt="Picks" src="<%=UIFunctions.GetGraphUrl(curReport, UIFunctions.GetBetType(i), 30) %>" />
                    </th>   
                </tr>
                <%foreach (ReportLeagueRaceBet report in curReport)
                  { 
                      RaceDetailPayout payout = lr.GetFinishPosition(report.RaceDetail);
                %>
                    <tr>
                        <td align="center"><%=report.RaceDetail.PostPosition%></td>
                        <td><%=report.RaceDetail.Horse.Name%><%=(report.RaceDetailId == favorite.RaceDetailId) ? "*" : "" %></td>
                        <td align="center"><%=(payout != null) ? UIFunctions.GetBetType(payout.BetType).ToString() : "--"%></td>
                        <td align="center"><%=report.UserBetCount%></td>
                    </tr>
                <%} %>
            </table>
        </fieldset>
    <% } %>
    <p>
       *Denotes race favorite
    </p>
    <p>
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

