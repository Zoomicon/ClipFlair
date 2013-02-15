<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
    AutoEventWireup="true"
    CodeBehind="Discussion.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Discussion" 
    Theme="Default" %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Discussion" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionContainer.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
        <ZoneTemplate>
            <MonoX:Discussion ID="discussion" runat="server" IsPublic="true" EnablePrettyPhoto="true" EnableHtmlEditor="true" 
            EnableContentSharing="true" EnableAnswering="true" EnableAnsweredTopicAutoClose="false" EnableRating="true" EnableTags="true" 
            EnableSyntaxHighlighter="true" MaximumTags="0" EnableSubscription="true" EnableAutoSubscription="true"
            EnableBoardMembership="true" EnableDailyReport="true" EnableOwnerDeleteOperation="false" EnableXSSSecurityParser="true"
            RatingHistoryVisible="false" BoardNames=""  GravatarRenderType="NotSet"
                />                        
        </ZoneTemplate>
    </portal:PortalWebPartZoneTableless>
</asp:Content>
