<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StyledButton.ascx.cs"
    Inherits="MonoSoftware.MonoX.Controls.StyledButton" %>
    
<asp:PlaceHolder ID="panButton" runat="server">
    <input type="submit" value="" style="display: none;" id="<%= this.ClientID %>" name="<%= this.UniqueID %>"
        onclick='<%= GetPostBackEventReference()  %>' />
    <div class="styled-button <%= CssClass %>">
        <ul class="button">
            <li style="margin-right: 2px; margin-left: 2px;">
                <asp:LinkButton runat="server" ID="btnButton"><span><%= Text %></span></asp:LinkButton>
                <asp:HyperLink runat="server" ID="btnLink"><span><%= Text %></span></asp:HyperLink>
            </li>
        </ul>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="panNativeButton" runat="server">
    <asp:Button ID="btnNativeButton" runat="server" />
</asp:PlaceHolder>