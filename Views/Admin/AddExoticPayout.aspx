<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.Domain.LeagueRace>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.Domain" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Add Exotic Payout</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% BetTypes betType = (BetTypes)this.ViewData["BetType"]; %>
    <h2>Add <%=betType.ToString() %> Payout</h2>

    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>

    <% LeagueRace leagueRace = this.ViewData.Model; %>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="Amount">Amount:</label>
                <%= Html.TextBox("Amount") %>
                <%= Html.ValidationMessage("Amount", "*") %>
            </p>
           
            <p>
                <input type="submit" value="Add" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

