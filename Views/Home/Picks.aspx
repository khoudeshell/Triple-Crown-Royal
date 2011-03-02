<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.Domain.LeagueRace>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Picks</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Set Picks</h2>

<%
    string message = this.ViewData["SuccessMessage"] == null ? String.Empty : this.ViewData["SuccessMessage"].ToString();
    if (message.Length > 0)
    {
%>
    <div id="message" class="success">
        <%=message %>
    </div>        
<%        
    }
%>
    <div id="picks">
        <% Html.RenderPartial("UserPicksUserControl");  %>
    </div> 
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

