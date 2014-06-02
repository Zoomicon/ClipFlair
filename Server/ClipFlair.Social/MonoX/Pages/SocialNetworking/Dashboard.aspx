<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Dashboard"
    MasterPageFile="~/MonoX/MasterPages/Default.master"
    MaintainScrollPositionOnPostback="true" %>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Wall" Src="~/MonoX/ModuleGallery/SocialNetworking/WallNotes.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="Events" Src="~/MonoX/ModuleGallery/SocialNetworking/Events.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="FriendSuggestionsList" Src="~/MonoX/ModuleGallery/SocialNetworking/FriendSuggestionsList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostList" Src="~/MonoX/ModuleGallery/Blog/BlogPostList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="NewGroups" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/NewGroupsList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="DiscussionTopic" Src="~/MonoX/ModuleGallery/SocialNetworking/Discussion/DiscussionTopics.ascx" %>  
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">   
    
    <div class="container-highlighter" style="background-color:#338061">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>Welcome to the ClipFlair Community</p>
                                </DefaultContent>
                            </MonoX:Editor>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>              
    </div>
    <div class="container">
        <div class="row-fluid clearfix">
            <div class="span12">
                <portal:PortalWebPartZoneTableless ID="map" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="Map">
                    <ZoneTemplate>
                        <MonoX:Editor runat="server" ID="editor05" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                            <DefaultContent>
                                <p>add a map</p>
                            </DefaultContent>
                        </MonoX:Editor>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
    <div class="container">            
            <div class="row-fluid">
                <div class="span8">
                    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_SocialZone %>' ID="activityPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:Wall runat="server" ID="snWallNotes" UsePrettyPhoto="true" ShowRating="false" GravatarRenderType="NotSet" WallNoteListVisible="false"  />                        
                        <MonoX:Events ID="ctlEvents" runat="server" PageSize="10" PagingEnabled="true" ></MonoX:Events>
                    </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
                <div class="span4">
                    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_SocialZone %>' ID="connectPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:FriendSuggestionsList ID="friendSuggestionsList" runat="server" NumberOfFriendSuggestions="6" ShowSearchBox="true" SuggestionMode="Shorten"></MonoX:FriendSuggestionsList>
                        <MonoX:DiscussionTopic runat="server" ID="ctlNewTopics" EnableInsertNewTopic="false" EnableAnswering="false" EnableOwnerDeleteOperation="false" EnablePaging="false" 
                            TopicSorter="SortNewTopicsOnTop" ShowBackLink="false" TopicFilterType="LastActiveTopics" Template="DiscussionTopicList" PageSize="5" HeaderVisible="false" EnableDiscussionDefaultUrl="true"  />
                        <MonoX:NewGroups runat="server" ID="ctlNewGroups" CacheDuration="600" PagingEnabled="false" MaxDescriptionChars="90" AvatarSize="32" PageSize="5"></MonoX:NewGroups>  
                        <MonoX:BlogPostList ID="ctlBlogPostList" RetainBreaksInDescription="false" CacheDuration="600" RewritePageTitle="false" Template="BlogPostListShort" runat="server" RssEnabled="true" IsHeaderVisible="False" MaxDescriptionChars="90" PageSize="5" MaxTitleChars="40" PagingEnabled="false" />
                    </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        
    </div>
</asp:Content>