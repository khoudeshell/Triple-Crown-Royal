<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.Domain.LeagueRace>" %>
<%@ Import Namespace="HorseLeague.Models.Domain" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Fix Picks</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Fix Picks</h2>

    <p>
        Scratches: <%=UIFunctions.GetScratchString(this.Model)%>
    </p>
    <p>
        Odds Order: 
        <%  
            IList<RaceDetail> rds = this.Model.RaceDetailsByOdds;

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
        <%=Html.ActionLink("Back to List", "ViewLeagueRace", new { id = this.Model.Id })%>
    </div>
</asp:Content>
