<%@ Page 
    Title=""
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.UserProfile"
    %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="PeopleSearch" Src="~/MonoX/ModuleGallery/SocialNetworking/PeopleSearch.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="EditProfile" Src="~/MonoX/ModuleGallery/ProfileModule/UserProfileModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="DiscussionMessages" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionMessages.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="InvitationList" Src="~/MonoX/ModuleGallery/SocialNetworking/InvitationList.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                <ZoneTemplate>
                    <MonoX:EditProfile id="ctlProfile" runat="server" AutoDetectUser="false" HiddenFieldsString="FirstName,LastName" GravatarRenderType="NotSet" >
                        <EditTemplate>                                
                        </EditTemplate>
                        <PreviewTemplate>
                        </PreviewTemplate>
                    </MonoX:EditProfile>
                </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>
        </div>
        <div class="row-fluid">
            <div class="span8">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone1" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:Wall runat="server" ID="snWallNotes" UsePrettyPhoto="true" GravatarRenderType="NotSet" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
            <div class="span4">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:PeopleSearch runat="server" ID="snPeopleSearch" ConfirmationRequired="true" />
                        <MonoX:InvitationList runat="server" InvitationType="InvitationsSent" ID="ctlInvitationsSent" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                        <MonoX:InvitationList runat="server" InvitationType="InvitationsReceived" ID="ctlInvitationsReceived" PageSize="9" AvatarSize="40" HideIfEmpty="true" />
                        <MonoX:FriendList runat="server" PageSize="9" AvatarSize="40" ID="snFriendList"></MonoX:FriendList>
                        <MonoX:DiscussionMessages ID="discussionTopicMessages" runat="server" HideIfEmpty="true" ShowActionButtons="false" ShowMessagePost="false" CurrentMode="None" IsPublic="true" 	EnablePrettyPhoto="true" MaxPostLength="40" Template="UserProfileDiscussionMessage" EnableSyntaxHighlighter="false" ShowHeader="false" ShowPager="false" PageSize="10"></MonoX:DiscussionMessages>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
</asp:Content>
