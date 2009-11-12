<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.DataAccess.RaceDetailPayout>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Add Payout</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% string payoutType = Enum.Parse(typeof(BetTypes), this.Model.BetType.ToString()).ToString(); %>
    <h2>Add <%=payoutType %> Payout</h2>

    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>

    <% LeagueRace leagueRace = (LeagueRace)this.ViewData["LeagueRace"]; %>
        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="RaceDetailId">Horse:</label>
                <%= Html.DropDownList("RaceDetailId", UIFunctions.PopulateHorseDropDown(leagueRace)) %>
                <%= Html.ValidationMessage("RaceDetailId", "*") %>
            </p>
            <%
                if(Convert.ToBoolean(this.ViewData["IsWinEnabled"]))
                {
            %>        
                <p>
                    <label for="WinAmount">Win Amount:</label>
                    <%= Html.TextBox("WinAmount") %>
                    <%= Html.ValidationMessage("WinAmount", "*") %>
                </p>
             <%
                }
                if (Convert.ToBoolean(this.ViewData["IsPlaceEnabled"]))
                {    
             %>
            <p>
                <label for="PlaceAmount">Place Amount:</label>
                <%= Html.TextBox("PlaceAmount")%>
                <%= Html.ValidationMessage("PlaceAmount", "*")%>
            </p>
            <%
                 }    
            %>
            <p>
                <label for="ShowAmount">Show Amount:</label>
                <%= Html.TextBox("ShowAmount") %>
                <%= Html.ValidationMessage("ShowAmount", "*") %>
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

