<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeoLoginStatus.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.SeoLoginStatus" %>
    
<asp:PlaceHolder ID="panStatus" runat="server">
    <%--!!!title NEEDS LOCALIZATION--%>
    <asp:LinkButton runat="server" ID="btnButton" CssClass="login-status" title="exit">
        <span><%= LogoutText %></span>        
    </asp:LinkButton>
    <asp:HyperLink runat="server" ID="btnLink"><%= LoginText %></asp:HyperLink>
</asp:PlaceHolder>
