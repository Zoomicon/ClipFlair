<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/MonoX/MasterPages/Default.master" 
    AutoEventWireup="true" 
    CodeBehind="PhotoGallery.aspx.cs" 
    Inherits="MonoSoftware.MonoX.Pages.SocialNetworking.PhotoGallery" 
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>   
<%@ Import Namespace="MonoSoftware.MonoX.Resources" %>
<%@ Register TagPrefix="MonoX" TagName="NewAlbumsList" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/NewAlbumsList.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="PhotoGalleryContainer" Src="~/MonoX/ModuleGallery/SocialNetworking/PhotoGallery/PhotoGalleryContainer.ascx" %>
<%@ Register Assembly="MonoX" Namespace="MonoSoftware.MonoX" TagPrefix="portal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cp" runat="server">
    <div class="photos">
        <MonoX:NewAlbumsList runat="server" ID="snPhotoGalleryNewAlbums" IsPagerVisible="false" ShowOnlyAlbumsWithCover="false" SortBy="SortByLastActivityDate" CacheDuration="0" PageSize="8" PagingEnabled="false" Visible="false" />
    </div>
    <div class="row-fluid">
        <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_LeftPartZone %>' ID="leftPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm">
            <ZoneTemplate>                        
                <MonoX:PhotoGalleryContainer runat="server" ID="snPhotoGallery" UsePrettyPhoto="true" GravatarRenderType="NotSet" ShowRating="true"  />
            </ZoneTemplate>
        </portal:PortalWebPartZoneTableless>
            
        <portal:PortalWebPartZoneTableless HeaderText='<%$ Code: PageResources.Zone_RightPartZone %>' ID="rightWebPartZone" runat="server" Width="100%" ChromeTemplateFile="Standard.htm" ShowChromeForNonAdmins="true">
            <ZoneTemplate>
            </ZoneTemplate>
        </portal:PortalWebPartZoneTableless>
    </div>
</asp:Content>
