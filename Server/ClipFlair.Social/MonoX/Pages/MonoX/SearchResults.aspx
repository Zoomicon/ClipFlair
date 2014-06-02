<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" 
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Title="" 
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>  
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Search" Src="~/MonoX/ModuleGallery/MonoXSearchResult.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.SearchEngine" TagPrefix="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="container-highlighter" style="background-color:#338061">
        <div class="container">
            <div class="row-fluid clearfix">
                <div class="span12">
                    <p><%= PageResources.Title_SearchResults %></p>
                </div>
            </div>
        </div>              
    </div>
    <div class="container">
        <div class="row-fluid">
            <div class="span12">    
                <div class="search-results">                    
                    <!--CLIPFLAIR<div class="separator"></div>-->
                    <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="false">
                        <ZoneTemplate>
                            <MonoX:Search ID="search1" runat="server" Title='<%$ Code: PageResources.Title_SearchResults %>'>
                                <SearchProviderItems>
                                    <Search:SearchProviderItem Name="PagesSearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="BlogSearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="DiscussionSearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="NewsSearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="UserProfileSearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="PhotoGallerySearchProvider" Template="Default"></Search:SearchProviderItem>
                                    <Search:SearchProviderItem Name="GroupSearchProvider" Template="Default"></Search:SearchProviderItem>
                                            <Search:SearchProviderItem Name="CalendarEventSearchProvider" Template="Default"></Search:SearchProviderItem>
                                </SearchProviderItems>
                            </MonoX:Search>
                        </ZoneTemplate>
                    </portal:PortalWebPartZoneTableless>
                </div>
            </div>
        </div>
    </div>
</asp:Content>