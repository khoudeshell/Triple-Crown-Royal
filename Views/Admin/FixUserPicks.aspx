<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.UserLeagueRacePicksDomain>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Fix Picks</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Fix Picks</h2>

    <p>
        Scratches: <%=UIFunctions.GetScratchString(this.Model.LeagueRace)%>
    </p>
    <p>
        Odds Order: 
        <%  
            IList<RaceDetail> rds = this.Model.LeagueRace.RaceDetailsByOdds;

            foreach (RaceDetail rd in rds)
            {
        %>
                <%=UIFunctions.FormatHorseNameForDisplay(rd)%>,
        <%        
            }
        %>
    </p>
    <div id="picks">
        <% Html.RenderPartial("UserPicksUserControl");  %>
    </div> 
    
    <div>
        <%=Html.ActionLink("Back to List", "ViewLeagueRace", new { id = this.Model.LeagueRace.Id })%>
    </div>
</asp:Content>
