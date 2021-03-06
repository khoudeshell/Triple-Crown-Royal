﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models.Domain" %> 
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
                UserLeague userDomain = ViewData["UserDomain"] as UserLeague;
                
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
                    %>
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
                        <td><%=Html.Encode(lr.Race.Name)%></td>
                        <td align="center"><%=Html.Encode(lr.Race.Track)%></td>
                        <td align="center"><%=lr.Weight%></td>
                        <td align="center"><%=lr.PostTimeEST%></td>
                        <td align="center" class="table-cell-action"><a href="<%=lr.FormUrl %>" target="_blank">Get Past Performances</a> | <%= picksSelected == true ? Html.ActionLink("Update Picks", "Picks", new { id = lr.Id }) : Html.ActionLink("Set Picks", "Picks", new { id = lr.Id })%></td>
                    </tr>
            <% i++;
                    }
                } %>
            </table> 
        </fieldset>
    </div>       
</asp:Content>

