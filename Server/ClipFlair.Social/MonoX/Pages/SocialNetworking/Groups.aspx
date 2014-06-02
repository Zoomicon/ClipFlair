<%@ Page 
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="Groups.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Groups"
    MasterPageFile="~/MonoX/MasterPages/Default.master"    
%>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Import Namespace="MonoSoftware.MonoX" %><%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupPeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MemberList" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupMemberList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupInvitationList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GroupContainer" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupContainer.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GroupInfo" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupInfo.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="GroupSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/GroupSearch.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">

    <div class="container-highlighter" style="background-color:#417a77">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>Welcome to ClipFlair Groups</p>
                                </DefaultContent>
                            </MonoX:Editor>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>              
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="<%= IsHome ? "span8" : "span12" %>">
                <portal:PortalWebPartZoneTableless Orientation="Vertical" HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                    <ZoneTemplate>
                        <MonoX:GroupContainer runat="server" ID="ctlGroupContainer" GravatarRenderType="NotSet" />                        
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
            <asp:PlaceHolder ID="plhHome" runat="server">
                <div class="span4">
                    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                        <ZoneTemplate>
                            <MonoX:GroupInfo runat="server" CacheDuration="600" ID="ctlGroupInfo" />
                            <MonoX:GroupSearch runat="server" ID="ctlSearchGroups" />
                            <MonoX:MemberList runat="server" CacheDuration="600" PageSize="9" AvatarSize="40" ID="ctlGroupMemberList"></MonoX:MemberList>
                            <MonoX:PeopleSearch runat="server" CacheDuration="600" ConfirmationRequired="true" ID="ctlPeopleSearch" />
                            <MonoX:InvitationList runat="server" InvitationType="InvitationsSent" ID="ctlInvitationsSent" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                            <MonoX:InvitationList runat="server" InvitationType="InvitationsReceived" ID="ctlInvitationsReceived" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>