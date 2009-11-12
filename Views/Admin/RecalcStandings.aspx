<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>RecalcStandings</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>RecalcStandings</h2>
    
    <p>The standings have been recalculated.</p>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
