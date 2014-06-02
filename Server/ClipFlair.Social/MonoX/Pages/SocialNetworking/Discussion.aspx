<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    AutoEventWireup="true"
    CodeBehind="Discussion.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Discussion" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Discussion" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionContainer.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="container-highlighter" style="background-color:#7a4366">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>ClipFlair Forums.  Here you can create discussion topics and get answers. </p>
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
            <div class="span12">
                <div class="discussion">
                    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
                        <ZoneTemplate>
                            <MonoX:Discussion ID="discussion" runat="server" IsPublic="true" EnablePrettyPhoto="true" EnableHtmlEditor="true" 
                            EnableContentSharing="true" EnableAnswering="true" EnableAnsweredTopicAutoClose="false" EnableRating="true" EnableTags="true" 
                            EnableSyntaxHighlighter="true" MaximumTags="0" EnableSubscription="true" EnableAutoSubscription="true"
                            EnableBoardMembership="true" EnableDailyReport="true" EnableOwnerDeleteOperation="false" EnableXSSSecurityParser="true"
                            RatingHistoryVisible="false" BoardNames=""  GravatarRenderType="NotSet" />                        
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
