<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupPeopleSearch.ascx.cs" Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.GroupPeopleSearch" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupInvitationList.ascx" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server"></asp:ScriptManagerProxy>
<div class="people-search">
    <div class="input-form">
    <asp:Literal runat="server" ID="ltlInfoText"></asp:Literal> 
    <asp:Panel ID="pnlSearch" runat="server">
        <div class="search-textbox">
            <MonoX:UserSearch id="userSearch" runat="server" UserFilterMode="ShowFriends" AutoCompleteSeparator=""></MonoX:UserSearch>
        </div>
    </asp:Panel>
    </div> 
    <asp:Panel ID="pnlActionPanel" runat="server" CssClass="jq_monoxPeopleSearchCommand">
    <div style="float: right;">
    <MonoX:StyledButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" CssClass="SNbutton jq_monoXAddFriendAction"/>
    <MonoX:StyledButton runat="server" ID="btnViewProfile" OnClick="btnViewProfile_Click" CssClass="SNbutton"/>
    </div>
    <div style="clear:both">
    <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
    </div>
    <asp:Panel runat="server" ID="pnlInviteFriend" CssClass="jq_monoxInviteFriendPanel">
    <div class="personalMessageLabel"><asp:Literal runat="server" ID="lblPersonalMessage"></asp:Literal></div>
    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRequestMessage" Rows="4" Width="100%"></asp:TextBox>
    <div style="float: right;"><MonoX:StyledButton runat="server" ID="btnSendRequest" CssClass="SNbutton" OnClick="btnSendRequest_Click" /></div>
    </asp:Panel>
    <br />
    </asp:Panel>
</div>