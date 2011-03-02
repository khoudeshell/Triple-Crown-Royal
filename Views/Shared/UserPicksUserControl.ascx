<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<HorseLeague.Models.Domain.LeagueRace>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

    <fieldset>
        <legend>Race Info</legend>
        <table>
            <tr>
                <td>Race:</td>
                <td><a href="<%=this.Model.FormUrl %>" target="_blank"><%=Html.Encode(this.Model.Race.Name)%></a></td>
            </tr>
            <tr>
                <td>Track:</td>
                <td><%=this.Model.Race.Track%></td>
            </tr>
            <tr>
                <td>Weight:</td>
                <td><%=this.Model.Weight%></td>
            </tr>
            <tr>
                <td>Post Time:</td>
                <td><%=this.Model.PostTimeEST%></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <%= Html.ValidationSummary() %>

    <% 
        using (Html.BeginForm()) {
            HorseLeague.Models.Domain.UserLeague userLeague = this.ViewData["UserDomain"] as HorseLeague.Models.Domain.UserLeague;
            IList<HorseLeague.Models.Domain.UserRaceDetail> picks = userLeague.GetPicksForARace(this.Model);
    %>
        <fieldset>
            <legend>Picks</legend>
            <table>
                <tr>
                    <td>Win:</td>
                    <td>
                        <%=Html.DropDownList("cmbWin", UIFunctions.PopulateHorseDropDown(this.Model.RaceDetails, picks, 
                            BetTypes.Win, userLeague.User.Id)) %>
                        <%=Html.ValidationMessage("cmbWin")%>
                    </td>
                </tr>
                <tr>
                    <td>Place:</td>
                    <td>
                        <%=Html.DropDownList("cmbPlace", UIFunctions.PopulateHorseDropDown(this.Model.RaceDetails, picks,
                                                        BetTypes.Place, userLeague.User.Id))%>
                        <%=Html.ValidationMessage("cmbPlace")%>
                    </td>
                </tr>
                <tr>
                    <td>Show:</td>
                    <td>
                        <%=Html.DropDownList("cmbShow", UIFunctions.PopulateHorseDropDown(this.Model.RaceDetails, picks,
                                                        BetTypes.Show, userLeague.User.Id))%>
                        <%=Html.ValidationMessage("cmbShow")%>
                    </td>
                </tr>
                <tr>
                    <td>Back Up:</td>
                    <td>
                        <%=Html.DropDownList("cmbBackUp", UIFunctions.PopulateHorseDropDown(this.Model.RaceDetails, picks,
                                                        BetTypes.Backup, userLeague.User.Id))%>
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