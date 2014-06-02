<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StyledButton.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.StyledButton" %>
    
<asp:PlaceHolder ID="panButton" runat="server">
    <input type="submit" value="" style="display: none;" id="<%= this.ClientID %>" name="<%= this.UniqueID %>"
        onclick='<%= GetPostBackEventReference()  %>' />
        <asp:LinkButton runat="server" ID="btnButton"  CssClass="styled-button"><span><%= Text %></span></asp:LinkButton>
        <asp:HyperLink runat="server" ID="btnLink" CssClass="styled-button"><span><%= Text %></span></asp:HyperLink>

</asp:PlaceHolder>
<asp:PlaceHolder ID="panNativeButton" runat="server">
    <asp:Button ID="btnNativeButton" runat="server" />
</asp:PlaceHolder>

