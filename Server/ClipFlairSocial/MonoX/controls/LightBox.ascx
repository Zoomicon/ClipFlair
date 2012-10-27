<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LightBox.ascx.cs" Inherits="MonoSoftware.MonoX.Controls.LightBox" %>
<asp:Panel ID="panHold" runat="server">
    <table cellpadding="0" cellspacing="0" class="light-box-simple <%= CssSelector %>"
        style="width: <%= Width.ToString() %>; height: <%= Height.ToString() %>;">
        <tr>
            <td class="left-top-corner">
            </td>
            <td class="top">
            </td>
            <td class="right-top-corner">
            </td>
        </tr>
        <tr>
            <td class="left-side">
            </td>
            <td class="center-content">
                <asp:PlaceHolder ID="PlaceHolderContent" runat="server"></asp:PlaceHolder>
            </td>
            <td class="right-side">
            </td>
        </tr>
        <tr>
            <td class="left-bottom-corner">
            </td>
            <td class="bottom">
            </td>
            <td class="right-bottom-corner">
            </td>
        </tr>
    </table>
</asp:Panel>
