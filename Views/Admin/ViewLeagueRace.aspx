<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HorseLeague.Models.LeagueRaceDomain>" %>
<%@ Import Namespace="HorseLeague.Models" %>
<%@ Import Namespace="HorseLeague.Models.DataAccess" %> 
<%@ Import Namespace="HorseLeague.Views.Shared" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>ViewLeagueRace</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Administer Race</h2>
    
    <%= Html.ValidationSummary() %>

    <% using (Html.BeginForm()) {%>
        <fieldset>
            <legend>League Race</legend>
            <table width="100%">
                <tr>
                    <td>Race:</td>
                    <td><%=Html.Encode(this.Model.Race.Name)%></td>
                </tr>
                <tr>
                    <td>Track:</td>
                    <td><%=this.Model.Race.Track%></td>
                </tr>
                <tr>
                    <td>Post Time:</td>
                    <td><%= Html.TextBox("txtPost", this.Model.PostTime) %></td>
                </tr>
                <tr>
                    <td>Is Active:</td>
                    <td><%= Html.TextBox("txtIsActive", this.Model.IsActive) %></td>
                </tr>
                <tr>
                    <td>Form Url:</td>
                    <td><%= Html.TextBox("txtForm", this.Model.FormUrl) %></td>
                </tr>
            </table>
            
             <input type="submit" value="Save" />
        </fieldset>
       <% } %>
        
       <fieldset>
            <legend>Horses</legend>
            <table width="100%">
                <tr>
                    <th>Post</th>
                    <th>Name</th>
                    <th>Scratched?</th>
                    <th>Odds Order</th>
                </tr>
                <%
                    foreach (RaceDetail rd in this.Model.RaceDetails)
                    {
                %>
                        <tr>
                            <td align="center"><%=Html.ActionLink(rd.PostPosition.ToString(), "EditHorse", new { id = rd.RaceDetailId }) %></td>
                            <td><%=rd.Horse.Name %></td>
                            <td align="center"><%=rd.IsScratched %></td>
                            <td align="center"><%=rd.OddsOrder %></td>
                        </tr>
                <%
                    }  
                %>
            </table>
            <p>
                <%=Html.ActionLink("Add Horse", "AddHorse", new { id = this.Model.Id })%> 
            </p>
        </fieldset>

        <fieldset>
            <legend>Payouts</legend>
            <table width="100%">
                <tr>
                    <th>Post</th>
                    <th>Name</th>
                    <th>Win</th>
                    <th>Place</th>
                    <th>Show</th>
                </tr>
            <%  
                BetTypes betType = BetTypes.Show;
                for (int j = 1; j < 4; j++)
                {
                    switch (j)
                    {
                        case 1:
                            betType = BetTypes.Win;
                            break;
                        case 2:
                            betType = BetTypes.Place;
                            break;
                        case 3:
                            betType = BetTypes.Show;
                            break;
                    }

                    RaceDetailPayout curPayout = this.Model.GetPayout(betType);
                    if (curPayout != null)
                    {
                   %>
                        <tr>
                            <td align="center"><%=curPayout.RaceDetail.PostPosition%></td>
                            <td ><%=curPayout.RaceDetail.Horse.Name%></td>    
                            <td align="center"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.WinAmount)%></td>    
                            <td align="center"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.PlaceAmount)%></td>    
                            <td align="center"><%=UIFunctions.GetBetTypeValueFromPayout(curPayout.ShowAmount)%></td>    
                        </tr>            
                   <%
                    }
                    else
                    {
                   %>
                        <tr>
                            <td colspan="5"><%=Html.ActionLink(String.Format("Add {0} Payout", betType), "AddPayout", new { id = this.Model.Id, bet = betType })%></td>
                        </tr>
                   <%    
                    }
                } 
            %>
            </table>   
        </fieldset>
        
        <fieldset>
            <legend>Fix Users with Scratches</legend>
            <%
                IList<aspnet_User> invalidUsers = (IList<aspnet_User>)this.ViewData["UserScratches"];  
            
                if(invalidUsers.Count == 0)
                {    
            %>
                 <p>All users picks are good to go.</p>            
            <%
                }
                else
                {
                   foreach(aspnet_User user in invalidUsers)
                   {
                   %>
                        <p><%=Html.ActionLink(user.UserName, "FixUserPicks", new { id = this.Model.Id, userId = user.UserId } )%></p>
                   <%    
                    }
                } 
            %>
        </fieldset>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>
