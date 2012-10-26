<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeoLoginStatus.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.SeoLoginStatus" %>
    
<asp:PlaceHolder ID="panStatus" runat="server">
    <asp:LinkButton runat="server" ID="btnButton"><%= LogoutText %></asp:LinkButton>
    <asp:HyperLink runat="server" ID="btnLink"><%= LoginText %></asp:HyperLink>
</asp:PlaceHolder>
