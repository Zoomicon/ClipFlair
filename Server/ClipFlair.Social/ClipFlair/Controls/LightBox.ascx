<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LightBox.ascx.cs" Inherits="MonoSoftware.MonoX.Controls.LightBox" %>
<asp:Panel ID="panHold" runat="server">
    <div class="blogHeader <%= CssSelector %>">
                <asp:PlaceHolder ID="PlaceHolderContent" runat="server"></asp:PlaceHolder>
    </div>       
</asp:Panel>
