<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
    <title>Home Page</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <fieldset>
            <legend>Active Races</legend>
            <table width="100%"> 
                <tr>
                    <th>#</th>
                    <th>Picks<br />Entered</th>
                    <th>Race</th>           
                    <th>Track</th>           
                    <th>Weight</th>   
                    <th>Post Time</th>
                    <th>Actions</th>          
                </tr>
            <%  int i = 1;
                List<LeagueRace> lrs = ViewData["ActiveRaces"] as List<LeagueRace>;
                HorseLeague.Models.UserDomain userDomain = ViewData["UserDomain"] as HorseLeague.Models.UserDomain;
                
                if (lrs == null || lrs.Count == 0)
                {
                    %>
                     <tr>
                        <td colspan="6" align="center">There are no active races at this time.</td>
                    </tr>
                    <%
                }
                else
                {
                    foreach (LeagueRace lr in lrs)
                    {
                        HorseLeague.Models.LeagueRaceDomain leagueRaceDomain = new HorseLeague.Models.LeagueRaceDomain(lr); %>    
                    <tr>
                        <td><%=i%>.</td>
                        <td align="center">
                            <%  bool picksSelected = userDomain.HasUserSetPicksForRace(lr);
                                if (picksSelected)
                                { 
                            %>
                                    <input type="checkbox" disabled="disabled" checked="checked" />
                            <%
                                }
                                else
                                {
                            %>
                                    <input type="checkbox" disabled="disabled"  />
                            <%
                                }
                            %>
                        </td>
                        <td><%=Html.Encode(leagueRaceDomain.Race.Name)%></td>
                        <td align="center"><%=Html.Encode(leagueRaceDomain.Race.Track)%></td>
                        <td align="center"><%=leagueRaceDomain.Weight%></td>
                        <td align="center"><%=leagueRaceDomain.PostTimeEST%></td>
                        <td align="center" class="table-cell-action"><a href="<%=leagueRaceDomain.FormUrl %>" target="_blank">Get Past Performances</a> | <%= picksSelected == true ? Html.ActionLink("Update Picks", "Picks", new { id = lr.Id }) : Html.ActionLink("Set Picks", "Picks", new { id = leagueRaceDomain.Id })%></td>
                    </tr>
            <% i++;
                    }
                } %>
            </table> 
        </fieldset>
    </div>       
</asp:Content>

