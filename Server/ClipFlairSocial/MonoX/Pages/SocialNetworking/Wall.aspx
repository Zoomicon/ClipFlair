<%@ Page Language="C#"  
    AutoEventWireup="true"
    CodeBehind="Wall.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Wall"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    Theme="Default" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Events" Src="~/MonoX/ModuleGallery/SocialNetworking/Events.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/InvitationList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FollowerList" Src="~/MonoX/ModuleGallery/SocialNetworking/FollowerList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>        
        <td class="left-section">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                <ZoneTemplate>
                    <MonoX:Wall runat="server" ID="snWallNotes" UsePrettyPhoto="true" ShowRating="false" GravatarRenderType="NotSet"  />                        
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </td>
        <td class="right-section">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                <ZoneTemplate>
                    <MonoX:PeopleSearch runat="server" CacheDuration="600" ConfirmationRequired="true" ID="snPeopleSearch" />
                    <MonoX:InvitationList runat="server" InvitationType="InvitationsSent" ID="ctlInvitationsSent" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                    <MonoX:InvitationList runat="server" InvitationType="InvitationsReceived" ID="ctlInvitationsReceived" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                    
                    <MonoX:FriendList runat="server" CacheDuration="600" PageSize="18" AvatarSize="40" ID="snFriendList"></MonoX:FriendList>
                    <MonoX:Events ID="ctlEvents" runat="server"></MonoX:Events>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>            
        </td>
      </tr>
    </table>

</asp:Content>
