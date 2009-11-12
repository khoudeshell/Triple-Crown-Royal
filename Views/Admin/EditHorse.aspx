<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.DataAccess.RaceDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>EditHorse</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditHorse</h2>

    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            
            <p>
                <label for="PostPosition">Post Position:</label>
                <%= Html.TextBox("PostPosition", this.Model.PostPosition) %>
                <%= Html.ValidationMessage("PostPosition", "*") %>
            </p>
            <p>
                <label for="IsScratched">IsScratched:</label>
                <%= Html.TextBox("IsScratched", this.Model.IsScratched)%>
                <%= Html.ValidationMessage("IsScratched", "*") %>
            </p>
            <p>
                <label for="OddsOrder">OddsOrder:</label>
                <%= Html.TextBox("OddsOrder", this.Model.OddsOrder) %>
                <%= Html.ValidationMessage("OddsOrder", "*") %>
            </p>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "ViewLeagueRace", new { id = this.Model.LeagueRace.Id })%>
    </div>

</asp:Content>

