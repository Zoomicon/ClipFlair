<%@ Page
    Language="C#" 
    MasterPageFile="~/App_MasterPages/ClipFlair/DefaultPartiallyLocalized.master"
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Blog" 
    Title="MonoX Blog"
    Codebehind="Blog.aspx.cs" 
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogContainer" Src="~/MonoX/ModuleGallery/Blog/BlogContainer.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogCategories" Src="~/MonoX/ModuleGallery/Blog/BlogCategories.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="TagCloud" Src="~/MonoX/ModuleGallery/Blog/TagCloud.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogInfo" Src="~/MonoX/ModuleGallery/Blog/BlogInfo.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogList" Src="~/MonoX/ModuleGallery/Blog/BlogList.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" Runat="Server">
    <div class="container-highlighter" style="background-color:#517199">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <portal:PortalWebPartZoneTableless ID="HighlightBanner" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" HeaderText="HighlightBanner">
                        <ZoneTemplate>
                            <MonoX:Editor runat="server" ID="editor01" Title='<%$ Code: PageResources.Title_TopSection %>' DefaultDocumentTitle='<%$ Code: PageResources.Title_TopSection %>'>
                                <DefaultContent>
                                    <p>Welcome to the Blogs area</p>
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
            <div class="span8">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
                    <ZoneTemplate>
                        <MonoX:BlogContainer ID="blogContainer" runat="server" UsePrettyPhoto="true" DateFormatString="d" RelatedContentVisible="false" EnableSyntaxHighlighter="true" GravatarRenderType="NotSet" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
            <div class="span4">
                <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="RightColumn.htm" ShowChromeForNonAdmins="true">
                    <ZoneTemplate>
                        <MonoX:BlogInfo ID="blogInfo" CacheDuration="600" HideIfEmpty="true" runat="server" />
                        <MonoX:BlogCategories ID="blogCategories" CacheDuration="600" HideIfEmpty="true" runat="server" />
                        <MonoX:BlogList ID="blogList" CacheDuration="600" HideIfEmpty="true" runat="server" PageSize="5" Template="BlogListShort" />
                        <MonoX:TagCloud ID="tagCloud" CacheDuration="600" HideIfEmpty="true" runat="server" />
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </div>
        </div>
    </div>
</asp:Content>