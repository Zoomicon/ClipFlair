<%@ Control
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="BlockUser.ascx.cs"
    Inherits="MonoSoftware.MonoX.ModuleGallery.SocialNetworking.BlockUser" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/InvitationList.ascx" %>
<%@ Register Src="~/MonoX/controls/UserPicker.ascx" TagPrefix="MonoX" TagName="UserSearch" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>

<asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">
</asp:ScriptManagerProxy>

<div class="people-search input-form">
    <asp:Literal ID="ltlInfoText" runat="server" ></asp:Literal> 
    <asp:Panel ID="pnlSearch" runat="server" CssClass="PeopleSearch">
    <div class="search-textbox">
        <MonoX:UserSearch ID="userSearch" runat="server" UserFilterMode="ShowAllUsers" AutoCompleteSeparator="">
        </MonoX:UserSearch>
    </div>
    </asp:Panel>
    <asp:Panel ID="pnlActionPanel" runat="server" CssClass="jq_monoxPeopleSearchCommand">
        <div class="button-holder small-btn">
            <MonoX:StyledButton runat="server" ID="btnBlock" CausesValidation="true" OnClick="btnBlock_Click" CssClass="block-btn float-left" />
            <MonoX:StyledButton runat="server" ID="btnViewProfile" CausesValidation="false" OnClick="btnViewProfile_Click" CssClass="view-btn float-left" />
        </div>
    <div style="clear: both">
        <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
    </div>
    </asp:Panel>
</div>