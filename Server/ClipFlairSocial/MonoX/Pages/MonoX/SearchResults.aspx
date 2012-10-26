<%@ Page 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master" 
    AutoEventWireup="true"     
    Inherits="MonoSoftware.MonoX.BasePage" 
    Theme="Default"
    Title="" 
    %>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %> 
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>  
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="Search" Src="~/MonoX/ModuleGallery/MonoXSearchResult.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.SearchEngine" TagPrefix="Search" %>
<%@ Register TagPrefix="MonoX" TagName="LightBox" Src="~/ClipFlair/controls/LightBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <table cellspacing="0" cellpadding="0" class="search-page">
        <tr>
            <td valign="top">
                <MonoX:LightBox runat="server">
                    <ContentTemplate>                    
                        <h2><%= PageResources.Title_SearchResults %></h2>
                    </ContentTemplate>
                </MonoX:LightBox>
                <!--<div class="arrow-down"></div>-->
            </td>
        </tr>
        <tr>            
            <td>
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
                            </SearchProviderItems>
                        </MonoX:Search>
                    </ZoneTemplate>
                </portal:PortalWebPartZoneTableless>
            </td>
        </tr>
    </table>
</asp:Content>