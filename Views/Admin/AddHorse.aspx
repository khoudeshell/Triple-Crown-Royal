<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.Domain.RaceDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>AddHorse</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AddHorse</h2>

    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label>Horse:</label>
                <%= Html.TextBox("Horse") %>
                <%= Html.ValidationMessage("Horse", "*") %>
            </p>
            <p>
                <label for="PostPosition">Post:</label>
                <%= Html.TextBox("PostPosition") %>
                <%= Html.ValidationMessage("PostPosition", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "ViewLeagueRace", new { id = this.Model.LeagueRace.Id })%>
    </div>

</asp:Content>

