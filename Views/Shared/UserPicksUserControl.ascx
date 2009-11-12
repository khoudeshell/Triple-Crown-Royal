<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HorseLeague.Models.UserLeagueRacePicksDomain>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

    <fieldset>
        <legend>Race Info</legend>
        <table>
            <tr>
                <td>Race:</td>
                <td><a href="<%=this.Model.LeagueRace.FormUrl %>" target="_blank"><%=Html.Encode(this.Model.LeagueRace.Race.Name)%></a></td>
            </tr>
            <tr>
                <td>Track:</td>
                <td><%=this.Model.LeagueRace.Race.Track%></td>
            </tr>
            <tr>
                <td>Weight:</td>
                <td><%=this.Model.LeagueRace.Weight%></td>
            </tr>
            <tr>
                <td>Post Time:</td>
                <td><%=this.Model.LeagueRace.PostTime%></td>
            </tr>
        </table>
    </fieldset>
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>Picks</legend>
            <table>
                <tr>
                    <td>Win:</td>
                    <td>
                        <%=Html.DropDownList("cmbWin", UIFunctions.PopulateHorseDropDown(this.Model.LeagueRace.RaceDetails, this.Model.UserPicks, 
                            BetTypes.Win, this.Model.UserId)) %>
                        <%=Html.ValidationMessage("cmbWin")%>
                    </td>
                </tr>
                <tr>
                    <td>Place:</td>
                    <td>
                        <%=Html.DropDownList("cmbPlace", UIFunctions.PopulateHorseDropDown(this.Model.LeagueRace.RaceDetails, this.Model.UserPicks,
                            BetTypes.Place, this.Model.UserId))%>
                        <%=Html.ValidationMessage("cmbPlace")%>
                    </td>
                </tr>
                <tr>
                    <td>Show:</td>
                    <td>
                        <%=Html.DropDownList("cmbShow", UIFunctions.PopulateHorseDropDown(this.Model.LeagueRace.RaceDetails, this.Model.UserPicks,
                            BetTypes.Show, this.Model.UserId))%>
                        <%=Html.ValidationMessage("cmbShow")%>
                    </td>
                </tr>
                <tr>
                    <td>Back Up:</td>
                    <td>
                        <%=Html.DropDownList("cmbBackUp", UIFunctions.PopulateHorseDropDown(this.Model.LeagueRace.RaceDetails, this.Model.UserPicks,
                            BetTypes.Backup, this.Model.UserId))%>
                        <%=Html.ValidationMessage("cmbBackUp")%>
                    </td>
                </tr>
            </table>
            <p>
                <%if (Convert.ToBoolean(this.Model.IsUpdateable))
                  { 
                %>
                    <input type="submit" value="Save" />
                <%
                  }
                  else
                  { 
                %>
                    <input type="submit" value="Save" disabled="disabled"/>
                <%} %>
                    
            </p>
        </fieldset>

    <% } %>