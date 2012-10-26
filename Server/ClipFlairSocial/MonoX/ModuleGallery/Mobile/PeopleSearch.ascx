<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeopleSearch.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.Mobile.PeopleSearch" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/Mobile/InvitationList.ascx" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<div class="people-search">
    <asp:Literal runat="server" ID="ltlInfoText"></asp:Literal>
    <asp:Panel ID="pnlSearch" runat="server" CssClass="PeopleSearch">
    <div class="search-textbox">
        <MonoX:UserSearch ID="userSearch" runat="server" UserFilterMode="ShowAllUsers" AutoCompleteSeparator="">
        </MonoX:UserSearch>
    </div>
    </asp:Panel>
    <asp:Panel ID="pnlActionPanel" runat="server" CssClass="jq_monoxPeopleSearchCommand">
    <div data-inline="true">
    <MonoX:StyledButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" CssClass="SNbutton jq_monoXAddFriendAction" EnableNativeButtonMode="true" />
    <MonoX:StyledButton runat="server" ID="btnViewProfile" OnClick="btnViewProfile_Click" CssClass="SNbutton" EnableNativeButtonMode="true" />
    </div>
    <div style="clear: both">
        <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
    </div>
    <asp:Panel runat="server" ID="pnlInviteFriend" CssClass="jq_monoxInviteFriendPanel">
        <div class="personalMessageLabel">
            <asp:Literal runat="server" ID="lblPersonalMessage"></asp:Literal></div>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRequestMessage" Rows="4" Width="100%"></asp:TextBox>
            <MonoX:StyledButton runat="server" ID="btnSendRequest" CssClass="SNbutton" OnClick="btnSendRequest_Click" EnableNativeButtonMode="true" />
    </asp:Panel>
    <br />
    </asp:Panel>
</div>