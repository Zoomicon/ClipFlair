<%@ Page Title="" 
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true"
    CodeBehind="Messages.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Messages"     
%>
    
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MessageCenter" Src="~/MonoX/ModuleGallery/SocialNetworking/Messaging/MessageCenter.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td class="left-section">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>
                        <MonoX:MessageCenter runat="server" ID="messageList" UsePrettyPhoto="true" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </td>
            <td class="right-section">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:PeopleSearch runat="server" CacheDuration="600" ConfirmationRequired="true" ID="snPeopleSearch" />
                        <MonoX:FriendList runat="server" CacheDuration="600" PageSize="18" AvatarSize="40" ID="snFriendList">
                        </MonoX:FriendList>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </td>
        </tr>
    </table>
</asp:Content>
