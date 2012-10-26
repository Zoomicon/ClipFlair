<%@ Page
    Language="C#"
    AutoEventWireup="True"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.Dashboard"
    Theme="Default"
    MasterPageFile="~/MonoX/MasterPages/DefaultSmallHeader.master"
%>
        
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   

<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="Editor" Src="~/MonoX/ModuleGallery/MonoXHtmlEditor.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>
<%@ Register TagPrefix="MonoX" TagName="Search" Src="~/MonoX/ModuleGallery/MonoXSearchBox.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX.SearchEngine" TagPrefix="Search" %>
<%@ Register TagPrefix="MonoX" TagName="NewUsers" Src="~/MonoX/ModuleGallery/SocialNetworking/NewUsersList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="BlogPostList" Src="~/MonoX/ModuleGallery/Blog/BlogPostList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="NewGroups" Src="~/MonoX/ModuleGallery/SocialNetworking/Groups/NewGroupsList.ascx" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
<table cellpadding="0" cellspacing="0" class="three-columns">
    <tr>
        <td colspan="3" style="padding-bottom: 20px;">
            <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_SocialZone %>' ID="featuredProjectPartZone" runat="server" Width="100%" ChromeTemplateFile="LeftColumn.htm">
            <ZoneTemplate>
                <MonoX:Editor runat="server" ID="editor3" Title='<%$ Code: PageResources.Zone_SocialZone %>'>
                <DefaultContent>
                    <h1>Harness the Power of Social Networking in ASP.NET</h1>
                    <img class="lets-socialize-img" src="~/App_Themes/Default/img/social-networking-icon.png" alt="Social Networking" />
                    <p>Create vibrant online communities. Connect with your friends. Kick up your own groups and join people that share your interests. Post blog articles. Share photos and videos. Write wall notes and comment on other people's posts. Follow activity streams.</p>
                    <p>With a touch of your own creativity and innovative spirit, there are no limits to what you can do with MonoX. And best of all - you'll get it all for free.</p>
                </DefaultContent>
                </MonoX:Editor>
            </ZoneTemplate>
            </portal:PortalWebPartZoneTableless>            
        </td>
    </tr>    
	<tr>
    	<td class="first search">
        	<h3><a href="#"><%= PageResources.Label_Blogs %></a></h3>
            <div class="search-box">
            	<h2><%= PageResources.Label_BlogSearch %></h2>
                <MonoX:Search runat="server" ID="ctlSearchBlogs" ButtonCssClass="button" ButtonText="<%$ Code:DefaultResources.Search_Go %>" SearchButtonType="LinkButton" DefaultSearchText="<%$ Code:DefaultResources.SiteSearch_DefaultText %>" >
                    <SearchProviders>
                        <Search:SearchProviderItem Name="BlogSearchProvider" Template="Default" />
                    </SearchProviders>
                </MonoX:Search>
            </div>
            <div class="section">
                <MonoX:BlogPostList ID="ctlBlogPostList" RetainBreaksInDescription="false" CacheDuration="600" RewritePageTitle="false" Template="BlogListShort" runat="server" RssEnabled="true" IsHeaderVisible="False" MaxDescriptionChars="90" PageSize="4" MaxTitleChars="40" PagingEnabled="false" /> 
            </div>
            
        </td>
        <td class="second search">
        	<h3><a href="#"><%= PageResources.Label_Groups %></a></h3>
            <div class="search-box">
            	<h2><%= PageResources.Label_GroupSearch %></h2>
                <MonoX:Search runat="server" ID="ctlSearchGroups" ButtonCssClass="button" ButtonText="<%$ Code:DefaultResources.Search_Go %>" SearchButtonType="LinkButton" DefaultSearchText="<%$ Code:DefaultResources.SiteSearch_DefaultText %>" >
                    <SearchProviders>
                        <Search:SearchProviderItem Name="GroupSearchProvider" Template="Default" />
                    </SearchProviders>
                </MonoX:Search>
            </div>
            <div class="section">
                <MonoX:NewGroups runat="server" ID="ctlNewGroups" CacheDuration="600" PagingEnabled="false" MaxDescriptionChars="90" AvatarSize="32" PageSize="5"></MonoX:NewGroups>  
            </div>
        </td>
        <td class="third search">
        	<h3><a href="#"><%= PageResources.Label_People %></a></h3>
            <div class="search-box">
            	<h2><%= PageResources.Label_PeopleSearch %></h2>
                <MonoX:Search runat="server" ID="ctlSearchUsers" ButtonCssClass="button" ButtonText="<%$ Code:DefaultResources.Search_Go %>" SearchButtonType="LinkButton" DefaultSearchText="<%$ Code:DefaultResources.SiteSearch_DefaultText %>" >
                    <SearchProviders>
                         <Search:SearchProviderItem Name="UserProfileSearchProvider" Template="Default" />
                    </SearchProviders>
                </MonoX:Search>
            </div>
            <div class="people">
                <MonoX:NewUsers runat="server" ID="ctlNewUsers" CacheDuration="600" IsPagerVisible="false" ShowValidAvatarsOnly="true" SortBy="SortRandomly" PageSize="20" PagingEnabled="false" ></MonoX:NewUsers>
            </div>

        </td>
	</tr>
</table>
</asp:Content>    
