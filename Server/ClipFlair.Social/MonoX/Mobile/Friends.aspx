<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Friends.aspx.cs"
    Inherits="MonoSoftware.MonoX.Mobile.Friends"
    MasterPageFile="~/MonoX/MasterPages/Mobile/Default.master"
    Theme="Mobile"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>  
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/Mobile/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/Mobile/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/Mobile/InvitationList.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
<div class="friends-holder">
    <MonoX:PeopleSearch runat="server" CacheDuration="600" ConfirmationRequired="true" ID="snPeopleSearch" />
    <MonoX:InvitationList runat="server" InvitationType="InvitationsSent" ID="ctlInvitationsSent" PageSize="10" AvatarSize="40" HideIfEmpty="true" Caption='<%$ Code: PageResources.Module_InvitationsSent %>' />
    <MonoX:InvitationList runat="server" InvitationType="InvitationsReceived" ID="ctlInvitationsReceived" PageSize="10" AvatarSize="40" HideIfEmpty="true" Caption='<%$ Code: PageResources.Module_InvitationsReceived %>' />
    <MonoX:FriendList runat="server" CacheDuration="600" PageSize="10" AvatarSize="40" ID="snFriendList" Caption='<%$ Code: PageResources.Module_Friends %>'></MonoX:FriendList>
</div>
</asp:Content>