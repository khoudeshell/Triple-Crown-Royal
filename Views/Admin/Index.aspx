<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Index</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
        <legend>Races</legend>
        <table width="100%"> 
            <tr>
                <th>#</th>
                <th>Race</th>
                <th>Date</th>           
                <th>Track</th>           
            </tr>
        <% int i = 1;
            foreach (LeagueRace lr in (IEnumerable)ViewData["ScheduledRaces"])
           { %>    
                <tr>
                    <td><%=i%>.</td>
                    <td><%=Html.ActionLink(Html.Encode(lr.Race.Name), "ViewLeagueRace", new {id = lr.Id}, null)   %></td>
                    <td><%=lr.Dt.ToShortDateString() %></td>
                    <td align="center"><%=Html.Encode(lr.Race.Track) %></td>
                </tr>
        <% i++;
            } %>
        </table> 
        <p>
            <%=Html.ActionLink("Recalculate Standings", "RecalcStandings") %>
        </p>
    </fieldset>
    <br />
    <fieldset>
        <legend>Users</legend>
        <table width="100%"> 
            <tr>
                <th>#</th>
                <th>User</th>
                <th>Action</th>
            </tr>
        <% i = 1;
           foreach (aspnet_User user in (IEnumerable)ViewData["Users"])
           { %>    
                <tr>
                    <td><%=i%>.</td>
                    <td><%=Html.Encode(user.UserName)  %></td>
                    <td><%=Html.ActionLink("Reset Password", "ResetPassword", "Account", new { userName = user.UserName }, null)%></td>
                </tr>
        <% i++;
            } %>
        </table> 
    </fieldset>
</asp:Content>
