<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.UserLeagueRacePicksDomain>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Picks</title>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Set Picks</h2>

    <div id="picks">
        <% Html.RenderPartial("UserPicksUserControl");  %>
    </div> 

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

