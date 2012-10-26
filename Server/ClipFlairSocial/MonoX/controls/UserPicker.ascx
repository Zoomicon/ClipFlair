<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPicker.ascx.cs" Inherits="MonoSoftware.MonoX.Controls.UserPicker" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<rad:RadComboBox ID="ddlRecipients" Width="100%" AutoCompleteSeparator="," runat="server" HighlightTemplatedItems="true" EnableLoadOnDemand="true" ShowToggleImage="false" EnableEmbeddedSkins="false" MarkFirstMatch="true" AllowCustomText="false" ShowDropDownOnTextboxClick="false" Skin="AutoCompleteBox">
<ItemTemplate>
<%# GetItemTemplate() %>
<br />
</ItemTemplate>
<FooterTemplate>
<asp:Literal runat="server" ID="ltlTip"></asp:Literal>
</FooterTemplate>
</rad:RadComboBox>
