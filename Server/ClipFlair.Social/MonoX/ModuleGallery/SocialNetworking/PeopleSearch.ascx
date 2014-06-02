<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="PeopleSearch.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.PeopleSearch" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/InvitationList.ascx" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>
<!--CLIPFLAIR-->
<div class="people-search input-form">
    <!--<div class="ltlInfoText"><asp:Literal ID="ltlInfoText" runat="server" ></asp:Literal></div>-->
    <asp:Panel ID="pnlSearch" runat="server" CssClass="PeopleSearch">
        <div class="search-textbox">
            <MonoX:UserSearch ID="userSearch" runat="server" UserFilterMode="ShowAllUsers" AutoCompleteSeparator="">
            </MonoX:UserSearch>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlActionPanel" runat="server" CssClass="jq_monoxPeopleSearchCommand"> 
    <div class="small-btn">
        <br />
        <MonoX:StyledButton runat="server" ID="btnAdd" OnClick="btnAdd_Click" CssClass="jq_monoXAddFriendAction main-button add-btn float-left" />
        <MonoX:StyledButton runat="server" ID="btnBlock" OnClick="btnBlock_Click" CssClass="SNbutton block-btn float-left" />
        <MonoX:StyledButton runat="server" ID="btnViewProfile" OnClick="btnViewProfile_Click" CssClass="view-btn float:left" />
    </div>
    <div class=""><asp:Label runat="server" ID="lblMessage" CssClass="empty-message"><br /><br /></asp:Label></div>
    <asp:Panel runat="server" ID="pnlInviteFriend" CssClass="jq_monoxInviteFriendPanel">
        <dl>
            <dd><br /><br /><br />
                <asp:Literal runat="server" ID="lblPersonalMessage"></asp:Literal>
                <!--CLIPFLAIR-->
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRequestMessage" Rows="4" Width="100%" CssClass="margin-top"></asp:TextBox>
            </dd>
            <dd class="small-btn">
                <MonoX:StyledButton runat="server" ID="btnSendRequest" CssClass="SNbutton main-button submit-btn float-left" OnClick="btnSendRequest_Click" />
            </dd>
        </dl>
    </asp:Panel>
    </asp:Panel>
</div>